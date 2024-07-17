using Application.Services.Queries.DTOs;
using Application.Services.Wrappers;
using MediatR;

namespace Application.Services.Queries
{
    /// <summary>
    /// Query genérico que devuelve elementos de una página
    /// </summary>
    /// <typeparam name="T">Tipo de elementos a devolver</typeparam>
    public class PagedQuery<T>(int page, int pageSize) : IRequest<Result<PagedList<T>>>
    {
        /// <summary>
        /// Página
        /// </summary>
        public int Page { get; set; } = page;

        /// <summary>
        /// Tamaño de página
        /// </summary>
        public int PageSize { get; set; } = pageSize;
    }
}
