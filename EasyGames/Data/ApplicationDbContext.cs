using Microsoft.EntityFrameworkCore;

namespace EasyGames.Data
{
    // ApplicationDbContext implements or inherits from DbContext class, a built in class inside Entity framework
    public class ApplicationDbContext : DbContext // class using microsoft entity framework core
    {
        // ApplicationDbContext will be registerred in Program.cs
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)// constructor
        {
            
        }
    }
}
