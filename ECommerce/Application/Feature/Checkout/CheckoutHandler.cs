using ECommerce.Application.Common;
using ECommerce.Application.Interfaces;
using ECommerce.Domain.Entities.Orders;
using ECommerce.Domain.Entities.Payments;
using ECommerce.Domain.Entities.Products;
using MediatR;

namespace ECommerce.Application.Feature.Checkout
{
    /// <summary>
    /// Caso de uso encargado de ejecutar el proceso completo de Checkout.
    /// Orquesta la creación de la orden, reserva de inventario,
    /// generación del pago y confirmación final de la compra.
    /// </summary>
    public sealed class CheckoutHandler : IRequestHandler<CheckoutCommand, Result<Guid>>
    {
        private readonly ICartRepository _cartRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IInventoryRepository _inventoryRepository;
        private readonly IPaymentGateway _paymentGateway;
        private readonly IPaymentRepository _paymenyRepository;
        private readonly IUnitOfWork _unitOfWork;


        public CheckoutHandler(ICartRepository cartRepository,IOrderRepository orderRepository,IInventoryRepository inventoryRepository,
            IPaymentGateway paymentGateway, IPaymentRepository paymentRepository,  IUnitOfWork unitOfWork)
        {
            _cartRepository = cartRepository;
            _orderRepository = orderRepository;
            _inventoryRepository = inventoryRepository;
            _paymentGateway = paymentGateway;
            _paymenyRepository = paymentRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid>> Handle(CheckoutCommand command,CancellationToken cancellation)
        {
            //Se obtiene el carrito con sus items.
            //Si no existe o está vacío, no se permite continuar el proceso
            var cart = await _cartRepository.GetByIdCart(command.CartId, cancellation);

            if (cart is null || !cart.Items.Any()) return Result<Guid>.Failure("Carrito vacío");

            //Se crea la orden a partir de los items del carrito.
            //La entidad Order valida reglas de negocio internas.
            var orderResult = Order.Create(command.UserId, cart.Items);

            if (orderResult.IsFailure) return Result<Guid>.Failure(orderResult.Error);

            var order = orderResult.Value;

            //Se reserva inventario por cada producto de la orden.
            //Esto garantiza consistencia antes de procesar el pago.
            foreach (var item in order.Items)
            {
                var inventory = await _inventoryRepository.GetByProductId(item.ProductId, cancellation);

                if (inventory is null)return Result<Guid>.Failure("Inventario no encontrado");

                // La reserva es una regla de dominio que valida disponibilidad.
                var reserveResult = inventory.Reserve(item.Quantity);

                if (reserveResult.IsFailure)return Result<Guid>.Failure(reserveResult.Error);

               await _inventoryRepository.Update(inventory);
            }

            //Se persiste la orden y se limpia el carrito
            await _orderRepository.AddAsync(order, cancellation);

            cart.Clear();

           await  _cartRepository.Update(cart);

            //Se crea el ValueObject Money para validar monto y moneda.
            var money = Money.Create(order.GetTotal(), order.Currency);
            if (money.IsFailure) return Result<Guid>.Failure("Valor incorrecto");

            //Se crea el registro de pago asociado a la orden.
            var paymentcreate = Payment.Create(order.Id, money.Value);
            var payment = await _paymenyRepository.AddAsync(paymentcreate.Value, cancellation);

            //confirman cambios antes de llamar a servicio externo.
            //Esto asegura que la orden exista en base de datos.
            await _unitOfWork.SaveChangesAsync(cancellation);


            //Se procesa el pago mediante un gateway externo
            //es una simulacion notificando el pago a x entidad o servicio.
            var paymentResult = await _paymentGateway.ProcessAsync(order.Id, order.GetTotal(), order.Currency, cancellation);

            if (!paymentResult.IsSuccess)
            {
                // Si el pago falla, se marca como fallido.
                payment.MarkAsFailed();
                await _unitOfWork.SaveChangesAsync(cancellation);return Result<Guid>.Failure("Pago fallido");
            }

            //Si el pago es exitoso:
            //Se cierra el carrito lo que hace el modificar el estado a inactivo
            //Se marca la orden como pagada
            // Se confirma el pago con el transactionId externo y se actualiza a un estado completed
            cart.Close();                          
            order.MarkAsPaid();
            payment.MarkAsCompleted(paymentResult.TransactionId);
            
            await _unitOfWork.SaveChangesAsync(cancellation);

            return Result<Guid>.Success(order.Id);
        }

    }

}
