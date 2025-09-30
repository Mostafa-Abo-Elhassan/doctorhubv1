using System.Linq.Expressions;

namespace Infrastructure.GenericRepositoryPattern
{
    public interface IGenericRepositoryAsync<T> where T : class
    {

        Task<T> GetByIdAsync(object id);
        Task<IEnumerable<T>> GetAllAsync();
        Task AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
        IQueryable<T> Query(); // for filtering
        Task SaveChangesAsync();
        Task<int> CountAsync();
        Task<IEnumerable<T>> GetAllWithCareria(Expression<Func<T, bool>> carieria, string[] includes = null, CancellationToken cancellationToken = default);
        Task<IEnumerable<T>> GetAllWithIncludes(string[] includes = null, CancellationToken cancellationToken = default);

        Task<T> GetWithCareria(Expression<Func<T, bool>> carieria, string[] includes = null);
        Task<List<TResult>> GetFilteredProjectedAsync<TResult>(Expression<Func<T, bool>> filter, Expression<Func<T, TResult>> selector,
                                                                     CancellationToken cancellationToken = default);
        Task<List<TResult>> GetbyJustFiltered<TResult>( Expression<Func<T, TResult>> selector,
                                                                     CancellationToken cancellationToken = default);
        Task<List<TResult>> GetFilteredProjectedWithIncludesAsync<TResult>(Expression<Func<T, bool>> filter, Expression<Func<T, TResult>> selector,
                                                                     string[] includes = null,
                                                                                                 CancellationToken cancellationToken = default);
        Task<List<TResult>> GetGroupedAsync<TKey, TResult>(
    Expression<Func<T, TKey>> groupBy,
    Expression<Func<IGrouping<TKey, T>, TResult>> selector,
    CancellationToken cancellationToken = default);
    }
}
