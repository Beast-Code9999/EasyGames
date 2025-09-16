using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using EasyGames.DataAccess.Data;
using EasyGames.DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

// Generic repository implementation for common CRUD operations.
// Works with any entity type T using Entity Framework Core and ApplicationDbContext.
// Provides methods to add, retrieve, and remove records from the database.

namespace EasyGames.DataAccess.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _db;
        internal DbSet<T> dbSet;
        // Initialize repository with EF Core DbContext
        public Repository(ApplicationDbContext db)
        {
            _db = db;
            this.dbSet = _db.Set<T>();
        }

        // Insert a new entity
        public void Add(T entity)
        {
            dbSet.Add(entity);
        }

        // Get a single entity based on a filter 
        public T Get(Expression<Func<T, bool>> filter)
        {
            IQueryable<T> query = dbSet;
            query = query.Where(filter);
            return query.FirstOrDefault();
        }
        // Get all entities of type T
        public IEnumerable<T> GetAll()
        {
            IQueryable<T> query = dbSet;
            return query.ToList();
        }
        // Remove a single entity
        public void Remove(T entity)
        {
            dbSet.Remove(entity);
        }
        // Remove multiple entities
        public void RemoveRange(IEnumerable<T> entities)
        {
            dbSet.RemoveRange(entities);
        }
    }
}
