using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EasyGames.DataAccess.Repository.IRepository
{
    internal interface IRepository<T> where T : class
    {
        // T will be any generic model we want to perform a CRUD operation or interact with DB context
        // IRepository<T> defines a generic contract for CRUD operations (Create, Read, Update, Delete)
        // on any entity type T. It abstracts the data access logic away from controllers/services
        // making the code more maintainable, reusable, and testable. Implementations of this interface
        // It is a generic repository interface for common CRUD operations on any entity type T
        // Methods included:
        // - GetAll():       Returns all records of type T
        // - Get(filter):    Returns a single record of type T matching a condition
        // - Add(entity):    Inserts a new entity into the database
        // - Update(entity): Updates an existing entity
        // - Delete(entity): Removes a single entity from the database
        // - DeleteRange(entities): Removes multiple entities in a single operation

        IEnumerable<T> GetAll();
        T Get(Expression<Func<T, bool>> filter);

        void Add(T entity);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);
    }
}
