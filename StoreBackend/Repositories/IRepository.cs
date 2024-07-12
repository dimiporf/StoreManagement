using System;
using System.Collections.Generic;

namespace StoreBackend.Repositories
{
    // Generic repository interface with CRUD operations
    public interface IRepository<T> where T : class
    {
        // Retrieves all entities of type T
        IQueryable<T> GetAll();

        // Retrieves a single entity of type T by its ID
        T GetById(int id);

        // Inserts a new entity of type T into the repository
        void Insert(T entity);

        // Updates an existing entity of type T in the repository
        void Update(T entity);

        // Deletes an entity of type T from the repository by its ID
        void Delete(int id);

        // Saves changes made to the repository
        void Save();
    }
}
