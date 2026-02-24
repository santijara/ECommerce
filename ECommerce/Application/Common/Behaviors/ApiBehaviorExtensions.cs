using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ECommerce.Application.Common.Behaviors
{
    /// <summary>
    /// Filtro de excepciones para capturar errores de validación de FluentValidation
    /// y devolverlos en un formato consistente al cliente.
    /// </summary>
    public class ValidationExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is ValidationException ex)
            {
                var errors = ex.Errors
                    .GroupBy(e => e.PropertyName)
                    .Select(g => new
                    {
                        Field = g.Key,
                        Errors = g.Select(e => e.ErrorMessage)
                    });

                context.Result = new BadRequestObjectResult(
                    ApiResponse<object>.Fail("Errores de validación", errors)
                );

                context.ExceptionHandled = true;
            }
        }
    }
}
