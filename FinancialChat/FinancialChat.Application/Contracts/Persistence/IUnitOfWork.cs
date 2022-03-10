using FinancialChat.Domain.Common;

namespace FinancialChat.Application.Contracts.Persistence
{
    public interface IUnitOfWork : IDisposable
    {        
        IAsyncRepository<TEntity> Repository<TEntity>() where TEntity : BaseDomainModel;
        Task<int> Complete();


    }
}
