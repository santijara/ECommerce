using ECommerce.Application.Common;
using ECommerce.Application.Common.Exceptions;
using ECommerce.Domain.Exceptions;

namespace ECommerce.Middleware
{
    /// <summary>
    /// Middleware global para captura y manejo de excepciones en la API.
    /// Intercepta cualquier excepción no controlada que ocurra en la pipeline.
    /// Centraliza la conversión de excepciones en respuestas HTTP estandarizadas.
    /// </summary>
    public class MiddlewareExceptions
    {
        private readonly RequestDelegate _requestDelegate;
        private readonly ILogger<MiddlewareExceptions> _logger;

        /// <summary>
        /// Constructor del middleware
        /// </summary>
       
        public MiddlewareExceptions(RequestDelegate requestDelegate, ILogger<MiddlewareExceptions> logger)
        {
            _requestDelegate = requestDelegate;
            _logger = logger;
        }

        /// <summary>
        /// Invocado automáticamente por la pipeline de ASP.NET Core.
        /// Ejecuta el siguiente middleware dentro de un bloque try/catch
        /// Si ocurre una excepción, se delega a HandleExceptionAsync
        /// </summary>
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _requestDelegate(context);
            }
            catch (Exception ex)
            {
                // Loguea la excepción completa
                _logger.LogError(ex, ex.Message);

                // Maneja la excepción y construye respuesta HTTP estandarizada
                await HandleExceptionAsync(context, ex);
            }
        }

        /// <summary>
        /// Convierte excepciones en respuestas HTTP estandarizadas.
        /// Mapea tipos de excepción a códigos HTTP específicos
        /// Devuelve un JSON consistente usando ApiResponse
        /// </summary>
        private static async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";

            // Mapear excepciones personalizadas a status codes
            var response = ex switch
            {
                NotFoundException => (
                    StatusCodes.Status404NotFound,
                    ApiResponse<object>.Fail(ex.Message) // Devuelve JSON con mensaje de error
                ),

                ValidationExceptions => (
                    StatusCodes.Status400BadRequest,
                    ApiResponse<object>.Fail(ex.Message)
                ),

                ConflictExceptions => (
                    StatusCodes.Status409Conflict,
                    ApiResponse<object>.Fail(ex.Message)
                ),

                DomainExceptions => (
                    StatusCodes.Status422UnprocessableEntity,
                    ApiResponse<object>.Fail(ex.Message)
                ),

                //Excepción no esperada => Internal Server Error
                _ => (
                    StatusCodes.Status500InternalServerError,
                    ApiResponse<object>.Fail(ex.Message)
                )
            };

            // Asigna el status code HTTP
            context.Response.StatusCode = response.Item1;

            // Serializa el ApiResponse a JSON
            await context.Response.WriteAsJsonAsync(response.Item2);
        }
    }
}
