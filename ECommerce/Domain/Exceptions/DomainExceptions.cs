namespace ECommerce.Domain.Exceptions
{
    /// <summary>
    /// Clase para manejo de excepciones en el domain
    /// </summary>
    public class DomainExceptions: Exception
    {
        public DomainExceptions(string message):base(message) { }
    }
}
