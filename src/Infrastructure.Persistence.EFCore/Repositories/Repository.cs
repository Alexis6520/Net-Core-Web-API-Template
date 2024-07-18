using Domain.Services.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Persistence.EFCore.Repositories
{
    /// <summary>
    /// Implementación genérica de repositorio
    /// </summary>
    /// <typeparam name="TEntity">Tipo de entidad</typeparam>
    /// <param name="dbSet">DbSet</param>
    public class Repository<TEntity>(DbSet<TEntity> dbSet) : IRepository<TEntity> where TEntity : class
    {
        private readonly DbSet<TEntity> _dbSet = dbSet;

        public async Task AddAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            await _dbSet.AddAsync(entity, cancellationToken);
        }

        public async Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> filter = null, string include = null, CancellationToken cancellationToken = default)
        {
            var query = (IQueryable<TEntity>)_dbSet;

            if (filter != null)
            {
                query.Where(filter);
            }

            if (include != null)
            {
                foreach (var prop in include.Split(','))
                {
                    if (string.IsNullOrEmpty(prop))
                    {
                        throw new ArgumentException("Valor inválido", nameof(include));
                    }

                    query.Include(prop);
                }
            }

            return await query.ToListAsync(cancellationToken);
        }

        public async Task<TEntity> FindAsync(CancellationToken cancellationToken = default, params object[] keys)
        {
            return await _dbSet.FindAsync(keys, cancellationToken);
        }

        public void Remove(TEntity entity)
        {
            _dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            _dbSet.RemoveRange(entities);
        }
    }
}
