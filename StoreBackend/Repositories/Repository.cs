using StoreBackend.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreBackend.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly WarehouseContext _context;
        private readonly DbSet<T> _dbSet;

        public Repository(WarehouseContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public IEnumerable<T> GetAll()
        {
            return _dbSet.ToList();
        }

        public T GetById(int id)
        {
            return _dbSet.Find(id);
        }

        public void Insert(T entity)
        {
            _dbSet.Add(entity);
        }

        public void Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            T entity = _dbSet.Find(id);
            _dbSet.Remove(entity);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
