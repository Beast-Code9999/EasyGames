using EasyGames.Models;
using Microsoft.EntityFrameworkCore;

namespace EasyGames.DataAccess.Data
{
    // ApplicationDbContext implements or inherits from DbContext class, a built in class inside Entity framework
    public class ApplicationDbContext : DbContext // class using microsoft entity framework core
    {
        // ApplicationDbContext will be registerred in Program.cs
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)// constructor
        {
            
        }

        public DbSet<Category> Categories { get; set; } // Type = DbSet, Entity = Category class


        // Seeds initial Category data into the database when the model is created
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                    new Category { Id = 1, Name = "Books", DisplayOrder = 1 },
                    new Category { Id = 2, Name = "Games", DisplayOrder = 2 },
                    new Category { Id = 3, Name = "Toys", DisplayOrder = 3 }
                );
        }
    }
}
