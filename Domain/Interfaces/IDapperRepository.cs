using System.Linq.Expressions;

namespace Domain.Interfaces;

public interface IDapperRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAllAsync(Expression<Func<IQueryable<T>, IOrderedQueryable<T>>>? orderBy = null);
    Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate, Expression<Func<IQueryable<T>, IOrderedQueryable<T>>>? orderBy = null);
    Task<IEnumerable<TParent>> GetAllWithIncludeAsync<TParent, TChild>(
    string childTable,
    string parentKey,
    string childKey,
    Func<TParent, TChild, TParent> map,
    string? orderBy = null);
    Task<T?> GetByIdAsync(object id);
    Task<int> InsertAsync(T entity);
    Task<int> UpdateAsync(T entity);
    Task<int> DeleteAsync(object id);
}
