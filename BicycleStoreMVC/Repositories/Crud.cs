using BicycleStoreMVC.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace BicycleStoreMVC.Repositories
{
    public class Crud<T> : ICrud<T> where T : class
    {
        private BicycleStoreDbContext _context;
        private DbSet<T> table;      

        public Crud(BicycleStoreDbContext context)
        {
            _context = context;
            table = _context.Set<T>();
        }
        //Changed IENumerable -- ToList() to IQueryable -- AsQueryable()
        public IQueryable<T> GetAll()
        {
            return table.AsQueryable();
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
    }
}
