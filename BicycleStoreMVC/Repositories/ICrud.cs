using System.Collections.Generic;
using System.Linq;

namespace BicycleStoreMVC.Repositories
{
    public interface ICrud<T> where T : class
    {
        IQueryable<T> GetAll();
        T GetById(object id);
        void Insert(T entity);
        void Update(T entity);
        void Delete(object id);
        void Save(T entity);
        IQueryable<T> Table { get; }
    }
}
