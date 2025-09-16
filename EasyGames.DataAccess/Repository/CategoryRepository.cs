using EasyGames.DataAccess.Data;
using EasyGames.DataAccess.Repository.IRepository;
using EasyGames.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EasyGames.DataAccess.Repository
{
    // Repository implementation for the Category entity
    // Inherits generic CRUD functionality from Repository<T>
    // and adds Category-specific operations
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        // Reference to the application's database context
        private ApplicationDbContext _db;

        // injects the DbContext and passes it to the base repository as Constructor
        public CategoryRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public void Update(Category obj)
        {
            _db.Categories.Update(obj);
        }
    }
}
