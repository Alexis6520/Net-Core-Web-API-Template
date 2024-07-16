using System.Linq.Expressions;

namespace Domain.Services.Repositories
{
    /// <summary>
    /// Repositorio con funciones básicas para interactuar con las entidades del dominio
    /// </summary>
    public interface IRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// Marca la entidad para inserción de forma asíncrona
        /// </summary>
        /// <param name="entity">Entidad</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Tarea con la operación de inserción</returns>
        Task AddAsync(TEntity entity, CancellationToken cancellationToken = default);

        /// <summary>
        /// Busca una entidad por sus llaves de forma asíncrona
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <param name="keys"></param>
        /// <returns>Tarea que devuelve la entidad encontrada</returns>
        Task<TEntity> FindAsync(CancellationToken cancellationToken = default, params object[] keys);

        /// <summary>
        /// Busca todas las entidades que cumplen cierta condición de forma asíncrona
        /// </summary>
        /// <param name="filter">Condición</param>
        /// <param name="include">Propiedades a incluir separado por comas</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Tarea con la entidades coincidentes</returns>
        Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> filter = null, string include = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// Marca la entidad para su eliminación
        /// </summary>
        /// <param name="entity">Entidad</param>
        void Remove(TEntity entity);

        /// <summary>
        /// Marca una serie de entidades para su eliminación
        /// </summary>
        /// <param name="entities">Entidades</param>
        void RemoveRange(IEnumerable<TEntity> entities);
    }
}
