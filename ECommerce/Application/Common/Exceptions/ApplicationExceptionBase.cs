namespace ECommerce.Application.Common.Exceptions
{
    /// <summary>
    /// Clase base abstracta para todas las excepciones de la aplicación.
    /// Permite un manejo centralizado de errores específicos de negocio.
    /// </summary>
    public abstract class ApplicationExceptionBase : Exception
    {
        protected ApplicationExceptionBase(string message) : base(message) { }
    }
}
