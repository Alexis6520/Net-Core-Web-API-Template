using Domain.Entities;
using Domain.Services;
using Domain.Services.Repositories;
using Infrastructure.Persistence.EFCore.Repositories;

namespace Infrastructure.Persistence.EFCore.Services
{
    public class UnitOfWork(ApplicationDbContext dbContext) : IUnitOfWork
    {
        private readonly ApplicationDbContext _dbContext = dbContext;
        private IRepository<Donut> _donuts;

        public IRepository<Donut> Donuts => _donuts ??= new Repository<Donut>(_dbContext.Donuts);

        public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }

        public void Dispose()
        {
            _donuts = null;
            GC.SuppressFinalize(this);
        }
    }
}
