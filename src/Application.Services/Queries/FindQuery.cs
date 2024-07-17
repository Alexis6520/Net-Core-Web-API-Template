using Application.Services.Wrappers;
using MediatR;

namespace Application.Services.Queries
{
    /// <summary>
    /// Query genérico para buscar un elemento por identificador
    /// </summary>
    /// <typeparam name="T">Tipo a devolver</typeparam>
    /// <typeparam name="I">Tipo de identificador</typeparam>
    public class FindQuery<T, I>(I id) : IRequest<Result<T>>
    {
        public I Id { get; set; } = id;
    }
}
