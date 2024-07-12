using StoreBackend.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreBackend.Repositories
{
    // Generic repository implementation using Entity Framework
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly WarehouseContext _context;
        private readonly DbSet<T> _dbSet;

        // Constructor to initialize the repository with a specific DbContext
        public Repository(WarehouseContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        // Retrieves all entities of type T
        public IQueryable<T> GetAll()
        {
               return _dbSet.AsQueryable();
        }

        // Retrieves a single entity of type T by its ID
        public T GetById(int id)
        {
            return _dbSet.Find(id);
        }

        // Inserts a new entity of type T into the repository
        public void Insert(T entity)
        {
            _dbSet.Add(entity);
        }

        // Updates an existing entity of type T in the repository
        public void Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        // Deletes an entity of type T from the repository by its ID
        public void Delete(int id)
        {
            T entity = _dbSet.Find(id);
            _dbSet.Remove(entity);
        }

        // Saves changes made to the repository
        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
