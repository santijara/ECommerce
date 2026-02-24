using FluentValidation;
using MediatR;

namespace ECommerce.Application.Common.Behaviors
{
    /// <summary>
    /// Pipeline behavior de MediatR que intercepta todas las solicitudes (requests)
    /// y ejecuta las validaciones definidas con FluentValidation antes de que el handler se ejecute.
    /// </summary>
    /// <typeparam name="TRequest">Tipo de la solicitud (Command o Query)</typeparam>
    /// <typeparam name="TResponse">Tipo de respuesta esperada del handler</typeparam>
    public class ValidationBehavior<TRequest, TResponse>
     : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            if (_validators.Any())
            {
                var context = new ValidationContext<TRequest>(request);

                var validationResults = await Task.WhenAll(
                    _validators.Select(v => v.ValidateAsync(context, cancellationToken)));

                var errors = validationResults
                    .SelectMany(r => r.Errors)
                    .Where(e => e != null)
                    .ToList();

                if (errors.Any())
                    throw new ValidationException(errors);
            }

            return await next();
        }
    }

}
