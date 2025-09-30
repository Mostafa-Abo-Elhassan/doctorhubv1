using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.GenericRepositoryPattern
{
    public class GenericRepositoryAsync<T> : IGenericRepositoryAsync<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<T> _dbSet;

        public GenericRepositoryAsync(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task<T> GetByIdAsync(object id) => await _dbSet.FindAsync(id);
        public async Task<IEnumerable<T>> GetAllAsync() => await _dbSet.ToListAsync();
        public async Task AddAsync(T entity) => await _dbSet.AddAsync(entity);
        public void Update(T entity) => _dbSet.Update(entity);
        public void Delete(T entity) => _dbSet.Remove(entity);
        public IQueryable<T> Query() => _dbSet.AsQueryable();
        public async Task SaveChangesAsync() => await _context.SaveChangesAsync();

        //public async IEnumerable<T> GetAllWithCareria(Expression<Func<T, bool>> carieria, string[] includes = null)
        //{
        //    IQueryable<T> query = _dbSet;

        //    if (includes != null)
        //        foreach (var include in includes)
        //            query = query.Include(include);

        //    return await query.Where(carieria).ToList();
        //}

        public async Task<IEnumerable<T>> GetAllWithCareria(Expression<Func<T, bool>> carieria, string[] includes, CancellationToken cancellationToken = default)
        {
            IQueryable<T> query = _dbSet;

            if (includes != null)
                foreach (var include in includes)
                    query = query.Include(include);

            return await query.Where(carieria).ToListAsync(cancellationToken);
        }

        public Task<T> GetWithCareria(Expression<Func<T, bool>> carieria, string[] includes = null)
        {

            IQueryable<T> query = _dbSet;

            if (includes != null)
                foreach (var include in includes)
                    query = query.Include(include);

            return query.SingleOrDefaultAsync(carieria);
        }


        public async Task<List<TResult>> GetFilteredProjectedAsync<TResult>(Expression<Func<T, bool>> filter,
       Expression<Func<T, TResult>> selector,
                CancellationToken cancellationToken = default)
        {
            return await _dbSet
                .Where(filter)
                .Select(selector)
                .ToListAsync(cancellationToken);
        }
        public async Task<List<TResult>> GetbyJustFiltered<TResult>(
       Expression<Func<T, TResult>> selector,
                CancellationToken cancellationToken = default)
        {
            return await _dbSet
                .Select(selector)
                .ToListAsync(cancellationToken);
        }
        public async Task<List<TResult>> GetFilteredProjectedWithIncludesAsync<TResult>(
    Expression<Func<T, bool>> filter,
    Expression<Func<T, TResult>> selector,
    string[] includes = null,
    CancellationToken cancellationToken = default)
        {
            IQueryable<T> query = _dbSet;

            // Apply includes لو فيه
            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            // Apply filter + projection
            return await query
                .Where(filter)
                .Select(selector)
                .ToListAsync(cancellationToken);
        }


        public async Task<IEnumerable<T>> GetAllWithIncludes(string[] includes = null, CancellationToken cancellationToken = default)
        {
            IQueryable<T> query = _dbSet;

            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            return await query.ToListAsync(cancellationToken);
        }

        public async Task<int> CountAsync()
            => await _dbSet.CountAsync();




        public async Task<List<TResult>> GetGroupedAsync<TKey, TResult>(
    Expression<Func<T, TKey>> groupBy,
    Expression<Func<IGrouping<TKey, T>, TResult>> selector,
    CancellationToken cancellationToken = default)
        {
            return await _dbSet
                .GroupBy(groupBy)
                .Select(selector)
                .ToListAsync(cancellationToken);
        }


    }
}
