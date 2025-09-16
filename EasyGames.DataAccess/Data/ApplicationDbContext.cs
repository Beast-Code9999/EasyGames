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
        public DbSet<Product> Products { get; set; } // Type = DbSet, Entity = Category class


        // Seeds initial Category data into the database when the model is created
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Books", DisplayOrder = 1 },
                new Category { Id = 2, Name = "Games", DisplayOrder = 2 },
                new Category { Id = 3, Name = "Toys", DisplayOrder = 3 }
                );
            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    Name = "Harry Potter",
                    Description = "Harry Potter is the \"Boy Who Lived,\" a young wizard with distinctive green eyes, messy black hair, and a lightning bolt-shaped scar on his forehead, who discovers his true identity after surviving an attack by the dark wizard Lord Voldemort as a baby",
                    Price = 0.01m,
                    CategoryId = 1,
                    ImageUrl = ""
                },
                new Product
                {
                    Id = 2,
                    Name = "The Witcher 3",
                    Description = "The Witcher 3: Wild Hunt is an open-world action RPG where players control Geralt of Rivia, a monster hunter searching for his adoptive daughter, Ciri, who is being pursued by the otherworldly Wild Hunt.",
                    Price = 0.02m,
                    CategoryId = 2,
                    ImageUrl = ""
                },
                new Product
                {
                    Id = 3,
                    Name = "Yoyo",
                    Description = "Make Cool Tricks YOO",
                    Price = 0.03m,
                    CategoryId = 3,
                    ImageUrl = ""
                }
                );
        }
    }
}
