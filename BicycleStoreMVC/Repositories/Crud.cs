using BicycleStoreMVC.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace BicycleStoreMVC.Repositories
{
    public class Crud<T> : ICrud<T> where T : class
    {
        private ApplicationDbContext _context = null;
        private DbSet<T> table = null;      

        public Crud(ApplicationDbContext context)
        {
            _context = context;
            table = _context.Set<T>();
        }
        public IEnumerable<T> GetAll()
        {
            return table.ToList();
        }
        public T GetById(object id)
        {
            return table.Find(id);
        }
        public IQueryable<T> Table { get; }
        IQueryable<T> ICrud<T>.Table { get; }

        public void Delete(object id)
        {
            T existing = table.Find(id);
            table.Remove(existing);
            Save(existing);
        }
        public void Insert(T entity)
        {
            table.Add(entity);
            Save(entity);
        }
        public void Update(T entity)
        {
            table.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            Save(entity);
        }

        public void Save(T entity)
        {
            _context.SaveChanges();
        }

        IEnumerable<T> ICrud<T>.GetAll()
        {
            throw new System.NotImplementedException();
        }
    }
}
