using ECommerce.Application.Interfaces;
using ECommerce.Domain.Entities.Invoices;
using ECommerce.Domain.Entities.Orders.Events;
using MediatR;

namespace ECommerce.Application.Feature.Checkout.Events
{
    /// <summary>
    /// Handler encargado de reaccionar cuando una orden es pagada.
    /// Implementa lógica posterior al pago como la generación y envío de la factura.
    /// Forma parte del mecanismo de Domain Events.
    /// </summary>
    public sealed class OrderPaidDomainEventHandler : INotificationHandler<OrderPaidDomainEvent>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IUserService _userService;
        private readonly IInvoiceSender _invoiceSender;
        private readonly IUnitOfWork _unitOfWork;

        public OrderPaidDomainEventHandler(IOrderRepository orderRepository,IInvoiceRepository invoiceRepository,
            IInvoiceSender invoiceSender, IUserService userService, IUnitOfWork unitOfWork)
        {
            _orderRepository = orderRepository;
            _invoiceRepository = invoiceRepository;
            _userService = userService;
            _invoiceSender = invoiceSender;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(OrderPaidDomainEvent notification, CancellationToken cancellationToken)
        {
            //Idempotencia (evitar duplicados)
            var existingInvoice = await _invoiceRepository.GetByOrderIdAsync(notification.PaymentId, cancellationToken);

            if (existingInvoice is not null)
                return;

            //Traer la orden
            var order = await _orderRepository.GetByIdAsync(notification.PaymentId, cancellationToken);

            if (order is null)
                throw new InvalidOperationException("Order not found.");

            var user = await _userService.GetByIdUser(order.UserId);
            if (user is null) throw new InvalidOperationException("User not found.");

            //Crear factura 
            var invoice = Invoice.Create(order.Id,order.SubTotal,order.Tax,order.Total);

            //Guardarla
            await _invoiceRepository.AddAsync(invoice, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            //Enviarla al correo electronico registrado del usuario
            var result = await _invoiceSender.SendAsync(invoice,user.Email.Value, cancellationToken);

            if (result.IsSuccess)
            {
                //Si el envio es exitoso, actualiza a un estado valido
                invoice.MarkAsSent(result.Value);
            }
            else
            {
                //Si el envio es Fail, actualiza el estado y guarda la descripcion del error
                invoice.MarkAsFailed(result.Error);
            }

            //Persistir estado final
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }

}
