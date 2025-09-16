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
    // Repository implementation for the Product entity
    // Inherits generic CRUD functionality from Repository<T>
    // and adds Product-specific operations
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        // Reference to the application's database context
        private ApplicationDbContext _db;

        // injects the DbContext and passes it to the base repository as Constructor
        public ProductRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        // Finds a product by Id and updates its details if it exists
        public void Update(Product obj)
        {
            var objFromDb = _db.Products.FirstOrDefault(u => u.Id == obj.Id);
            if (objFromDb != null)
            {
                objFromDb.Name = obj.Name;
                objFromDb.Description = obj.Description;
                objFromDb.Price = obj.Price;
                if(obj.ImageUrl != null)
                {
                    objFromDb.ImageUrl = obj.ImageUrl;
                }
            }
        }
    }
}
