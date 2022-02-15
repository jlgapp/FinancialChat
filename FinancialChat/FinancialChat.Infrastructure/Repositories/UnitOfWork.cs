using FinancialChat.Application.Contracts.Persistence;
using FinancialChat.Domain.Common;
using FinancialChat.Infrastructure.Persistence;
using System.Collections;

namespace FinancialChat.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private Hashtable? _repositories;
        private readonly FinancialChatDbContext _context;

        public UnitOfWork(FinancialChatDbContext context)
        {
            _context = context;
        }

        public FinancialChatDbContext FinancialChatDbContext => _context;


        public async Task<int> Complete()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public IAsyncRepository<TEntity> Repository<TEntity>() where TEntity : BaseDomainModel
        {
            if (_repositories == null)
                _repositories = new Hashtable();

            var type = typeof(TEntity).Name;

            if (!_repositories.ContainsKey(type))
            {
                var repositoryType = typeof(RepositoryBase<>);
                var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), _context);
                _repositories.Add(type, repositoryInstance);
            }

#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
#pragma warning disable CS8603 // Possible null reference return.
            return (IAsyncRepository<TEntity>)_repositories[type];
#pragma warning restore CS8603 // Possible null reference return.
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
        }
    }

}
