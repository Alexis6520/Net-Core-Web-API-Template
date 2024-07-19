using Domain.Entities;
using Domain.Services.Repositories;

namespace Domain.Services
{
    /// <summary>
    /// Unidad de trabajo para interactuar con las entidades del dominio
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Donas
        /// </summary>
        IRepository<Donut> Donuts { get; }

        /// <summary>
        /// Guarda los cambios realizados en los repositorios de forma asíncrona
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns>Tarea que guarda los cambios</returns>
        Task SaveChangesAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Guarda los cambios realizados en los repositorios
        /// </summary>
        void SaveChanges();
    }
}
