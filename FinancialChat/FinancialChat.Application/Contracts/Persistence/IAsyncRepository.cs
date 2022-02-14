using FinancialChat.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FinancialChat.Application.Contracts.Persistence
{
    public interface IAsyncRepository<T> where T : BaseDomainModel
    {
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate);
        Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>>? predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            string? includeString = null,
            bool disableTracking = true,
            int? takeRecords = null);

        Task<IReadOnlyList<T>> GetAsyncObjectInclude(Expression<Func<T, bool>>? predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            List<Expression<Func<T, object>>>? includes = null,
            bool disableTracking = true);

        Task<T> GetByIdAsync(int id);
        //Task<T> AddAsync(T entity);
       // Task<T> UpdateAsync(T entity);
        //Task DeleteAsync(T entity);

        void AddEntity(T entity);
        void UpdateEntity(T entity);
        void DeleteEntity(T entity);
    }

}
