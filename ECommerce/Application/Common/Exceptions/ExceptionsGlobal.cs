using System.Globalization;

namespace ECommerce.Application.Common.Exceptions
{
    /// <summary>
    /// Excepción personalizada para manejar casos donde un recurso no fue encontrado.
    /// Ejemplo: un usuario, producto o pedido inexistente.
    /// </summary>

    public sealed class NotFoundException : ApplicationExceptionBase
    {
        public NotFoundException(String message) : base(message) { }
    }

    /// <summary>
    /// Excepción personalizada para errores de validación.
    /// Útil cuando una regla de negocio no se cumple.
    /// </summary>
    public sealed class ValidationExceptions : ApplicationExceptionBase
    {
        public ValidationExceptions(string message) : base(message) { }
    }

    /// <summary>
    /// Excepción personalizada para errores de validación.
    /// Útil cuando una regla de negocio no se cumple.
    /// </summary>
    public sealed class ConflictExceptions : ApplicationExceptionBase
    {
        public ConflictExceptions(string message) : base(message) { }
    }
}
