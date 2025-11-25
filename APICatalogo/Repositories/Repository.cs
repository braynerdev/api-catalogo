using APICatalogo.Context;
using APICatalogo.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace APICatalogo.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly AppDbContext _context;

        public Repository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IQueryable<T>> GetAllAsync()
        {
           return _context.Set<T>().AsNoTracking();
            
        }

        public async Task<T?> GetAsync(Expression<Func<T, bool>> predicate)
        {
           return await _context.Set<T>().FirstOrDefaultAsync(predicate);
        }

        public T Create(T entity)
        {
            _context.Set<T>().Add(entity);
            return entity;
        }

        public T Update(T entity)
        {
            _context.Update(entity);
            return entity; 
        }
    }
}
