using MediatR;

namespace ECommerce.Domain
{
    /// <summary>
    /// Clase base para agregados (Aggregate Root) en DDD.
    /// Todos los agregados deben heredar de esta clase para gestionar eventos de dominio.
    /// Permite que la entidad principal controle sus propios eventos de negocio.
    /// Mantiene la integridad del agregado y facilita la propagación de cambios al dominio.
    /// </summary>
    public abstract class AgregateRoot
    {
        private readonly List<INotification> _domainEvents = new();

        public IReadOnlyCollection<INotification> DomainEvents
            => _domainEvents.AsReadOnly();

        protected void AddDomainEvent(INotification domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }

        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }
    }
}
