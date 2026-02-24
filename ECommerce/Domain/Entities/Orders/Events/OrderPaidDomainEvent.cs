using MediatR;

namespace ECommerce.Domain.Entities.Orders.Events
{
    public sealed record OrderPaidDomainEvent(Guid PaymentId, decimal Amount):INotification;
    
}
