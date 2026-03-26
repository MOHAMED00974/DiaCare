using Microsoft.EntityFrameworkCore;
using DiaCare.Domain.Interfaces;
using DiaCare.Infrastructure.Data;

namespace DiaCare.Infrastructure.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected AppDbContext _context;
        internal DbSet<T> _dbSet;

        public BaseRepository(AppDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public IQueryable<T> Query => _dbSet;

        public async Task<T> AddAsync(T item, CancellationToken cancellationToken = default)
        {
            await _dbSet.AddAsync(item, cancellationToken);
            return item;
        }

        public async Task AddRangeAsync(IEnumerable<T> values, CancellationToken cancellationToken = default)
        {
            await _dbSet.AddRangeAsync(values, cancellationToken);
        }
        public void Delete(T item)
        {
           
                _dbSet.Remove(item);
           
           
        }

        public async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default)
        {
           return await _dbSet.ToListAsync(cancellationToken);
        }
        //search in cash before datbase 
        public async Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _dbSet.FindAsync(new object[] { id }, cancellationToken);
        }


        public void RemoveRange(IEnumerable<T> entities)
        {
            _dbSet.RemoveRange(entities);
        }

        public void Update(T item)
        {
           
                _dbSet.Update(item);
        }
    }
}
