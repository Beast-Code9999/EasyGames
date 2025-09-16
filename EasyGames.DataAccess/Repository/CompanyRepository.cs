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
    // Repository implementation for the Company entity
    // Inherits generic CRUD functionality from Repository<T>
    // and adds Company-specific operations
    public class CompanyRepository : Repository<Company>, ICompanyRepository
    {
        // Reference to the application's database context
        private ApplicationDbContext _db;

        // injects the DbContext and passes it to the base repository as Constructor
        public CompanyRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Company obj)
        {
            _db.Companies.Update(obj);
        }
    }
}
