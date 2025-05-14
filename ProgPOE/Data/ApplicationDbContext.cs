using Microsoft.EntityFrameworkCore;
using ProgPOE.Models;
using System;

namespace ProgPOE.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Farmer> Farmers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure relationships
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Farmer)
                .WithMany(f => f.Products)
                .HasForeignKey(p => p.FarmerId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>()
                .HasOne(u => u.Farmer)
                .WithMany()
                .HasForeignKey(u => u.FarmerId)
                .OnDelete(DeleteBehavior.SetNull);

            // Get a fixed date for seeding to avoid issues with DateTime.Now in migrations
            var seedDate = new DateTime(2025, 5, 13);

            // Seed data for users (including at least one employee and one farmer)
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    Username = "employee",
                    Password = "employee123", // Using plain text password to avoid hashing issues
                    Role = UserRoles.Employee,
                    RegistrationDate = seedDate
                },
                new User
                {
                    Id = 2,
                    Username = "farmer",
                    Password = "farmer123", // Using plain text password to avoid hashing issues
                    Role = UserRoles.Farmer,
                    FarmerId = 1,
                    RegistrationDate = seedDate
                }
            );

            // Seed data for farmers
            modelBuilder.Entity<Farmer>().HasData(
                new Farmer
                {
                    Id = 1,
                    FirstName = "John",
                    LastName = "Smith",
                    Email = "john.smith@example.com",
                    PhoneNumber = "0123456789",
                    FarmName = "Green Acres Farm",
                    FarmLocation = "Eastern Cape",
                    RegistrationDate = seedDate.AddDays(-30)
                },
                new Farmer
                {
                    Id = 2,
                    FirstName = "Mary",
                    LastName = "Johnson",
                    Email = "mary.johnson@example.com",
                    PhoneNumber = "0987654321",
                    FarmName = "Sunrise Valley Farm",
                    FarmLocation = "Western Cape",
                    RegistrationDate = seedDate.AddDays(-15)
                }
            );

            // Seed data for products
            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    Name = "Organic Tomatoes",
                    Category = "Vegetables",
                    ProductionDate = seedDate.AddDays(-5),
                    Quantity = 100,
                    Unit = "kg",
                    PricePerUnit = 25.50m,
                    Description = "Fresh organic tomatoes",
                    ImageUrl = "https://images.pexels.com/photos/1327838/pexels-photo-1327838.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1",
                    FarmerId = 1
                },
                new Product
                {
                    Id = 2,
                    Name = "Free-range Eggs",
                    Category = "Dairy & Eggs",
                    ProductionDate = seedDate.AddDays(-2),
                    Quantity = 500,
                    Unit = "dozen",
                    PricePerUnit = 45.00m,
                    Description = "Farm-fresh free-range eggs",
                    ImageUrl = "https://images.pexels.com/photos/162712/egg-white-food-protein-162712.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1",
                    FarmerId = 1
                },
                new Product
                {
                    Id = 3,
                    Name = "Sweet Corn",
                    Category = "Vegetables",
                    ProductionDate = seedDate.AddDays(-3),
                    Quantity = 200,
                    Unit = "kg",
                    PricePerUnit = 15.75m,
                    Description = "Fresh sweet corn",
                    ImageUrl = "https://images.pexels.com/photos/603030/pexels-photo-603030.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1",
                    FarmerId = 2
                },
                new Product
                {
                    Id = 4,
                    Name = "Organic Apples",
                    Category = "Fruits",
                    ProductionDate = seedDate.AddDays(-7),
                    Quantity = 300,
                    Unit = "kg",
                    PricePerUnit = 18.50m,
                    Description = "Freshly picked organic apples",
                    ImageUrl = "https://images.pexels.com/photos/672101/pexels-photo-672101.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1",
                    FarmerId = 2
                }
            );
        }
    }
}