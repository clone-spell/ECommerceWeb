using ECommerceWeb.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ECommerceWeb.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly AppDbContext _context;
        private readonly DbSet<T> _dbSet;
        public Repository(AppDbContext context)
        {
            _context=context;
            _dbSet=_context.Set<T>();
        }


        public T Get(Expression<Func<T, bool>> expression)
        {
            return _dbSet.Where(expression).FirstOrDefault();
        }
        public IEnumerable<T> GetAll()
        {
            return _dbSet.ToList();
        }
        public IEnumerable<T> GetFirstN(int n, Expression<Func<T, object>> expression)
        {
            var len = _dbSet.Count();
            if (n > len)
                return _dbSet.OrderBy(expression).Take(len).ToList();

            return _dbSet.OrderBy(expression).Take(n).ToList();
        }
        public IEnumerable<T> GetLastN(int n, Expression<Func<T, object>> expression)
        {
            var len = _dbSet.Count();
            if (n > len)
                return _dbSet.OrderByDescending(expression).Take(len).ToList();

            return _dbSet.OrderByDescending(expression).Take(n).ToList();
        }
        public IEnumerable<T> GetMany(Expression<Func<T, bool>> expression)
        {
            return _dbSet.Where(expression).ToList();
        }
        public void Add(T item)
        {
            _dbSet.Add(item);
            _context.SaveChanges();
        }
        public void Update(T item)
        {
            _dbSet.Update(item);
            _context.SaveChanges();
        }
        public void Delete(Expression<Func<T, bool>> expression)
        {
            var cl = _dbSet.Where(expression).FirstOrDefault();
            if (cl != null)
            {
                _dbSet.Remove(cl);
                _context.SaveChanges();
            }
        }
    }
}
