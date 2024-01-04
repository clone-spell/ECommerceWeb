using System.Linq.Expressions;

namespace ECommerceWeb.Repository
{
    public interface IRepository<T> where T : class
    {
        T Get(Expression<Func<T, bool>> expression);
        IEnumerable<T> GetMany(Expression<Func<T, bool>> expression);
        IEnumerable<T> GetFirstN(int n,Expression<Func<T, object>> expression);
        IEnumerable<T> GetLastN(int n,Expression<Func<T, object>> expression);
        IEnumerable<T> GetAll();
        void Add(T item);
        void Update(T item);
        void Delete(Expression<Func<T, bool>> expression);

    }
}
