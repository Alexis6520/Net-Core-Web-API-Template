using Application.Services.Wrappers;
using FluentValidation;
using MediatR;
using System.Net;

namespace Application.Services.Behaviors
{
    /// <summary>
    /// Clase encargada de ejecutar las validaciones automáticamente
    /// </summary>
    /// <typeparam name="TRequest"></typeparam>
    /// <typeparam name="TResponse"></typeparam>
    /// <param name="validators"></param>
    public class ValidationBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators) : IPipelineBehavior<TRequest, TResponse> where TResponse : Result
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators = validators;

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (_validators.Any())
            {
                var context = new ValidationContext<TRequest>(request);
                var validationResults = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, cancellationToken)));
                var failures = validationResults.SelectMany(r => r.Errors).Where(f => f != null);

                if (failures.Any())
                {
                    var errors = failures.Select(x => x.ErrorMessage);
                    return (TResponse)Activator.CreateInstance(typeof(TResponse), false, HttpStatusCode.BadRequest, errors);
                }
            }

            return await next();
        }
    }
}
