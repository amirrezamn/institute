using System.Linq.Expressions;
using institute.DTOs;

namespace institute.Interfaces;

public interface IRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T?> GetByIdAsync(int id);
    Task AddAsync(T entity);
    Task UpdateAsync(T entity);

    Task SoftDeleteAsync(T entity);
    Task<List<T>> FindAsync(Expression<Func<T, bool>> predicate);
    Task HardDeleteAsync(T entity);

    Task<PagedResult<T>> Pagination<TKey>(int pageNumber, int pageSize,
        Expression<Func<T, TKey>> orderBy, bool ascending = true,
        Expression<Func<T, bool>>? filter = null, params Expression<Func<T, object>>[] includes);

    Task<IEnumerable<T>> FindIgnoreQueryFiltersAsync(Expression<Func<T, bool>> predicate);
}    
    
    