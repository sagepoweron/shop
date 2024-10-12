using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Shop.Logic;
using Shop.Models;
using System.Reflection.Emit;

namespace Shop.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        //protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        //{
        //    configurationBuilder
        //        .Properties<Currency>()
        //        .HaveConversion<CurrencyConverter>();
        //}

        public DbSet<Product> Product { get; set; } = default!;
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //builder.Entity<Product>().Property(p => p.ListPrice).HasPrecision(9, 4);

            Category game = new() { Name = "Game", DisplayOrder = 1 };
            Category book = new() { Name = "Book", DisplayOrder = 2 };
            Category other = new() { Name = "Other", DisplayOrder = 3 };

            builder.Entity<Category>().HasData(game, book, other);

            builder.Entity<Product>().HasData(
                new Product()
                {
                    CategoryId = game.Id,
                    Name = "Racing Game",
                    Brand = "Test",
                    ListPrice = 50,
                    SalePrice = 40
                },
                new Product()
                {
                    CategoryId = book.Id,
                    Name = "Book",
                    Brand = "Test",
                    ListPrice = 10,
                    SalePrice = 9
                },
                new Product()
                {
                    CategoryId = other.Id,
                    Name = "Bicycle",
                    Brand = "Test",
                    ListPrice = 100,
                    SalePrice = 90
                },
                new Product()
                {
                    CategoryId = other.Id,
                    Name = "Lamp",
                    Brand = "Test",
                    ListPrice = 30,
                    SalePrice = 20
                }
                );


            IdentityRole admin_role = new(Helpers.Admin_Role);
            IdentityRole customer_role = new(Helpers.Customer_Role);

            builder.Entity<IdentityRole>().HasData(admin_role, customer_role);


            //a hasher to hash the password before seeding the user to the db
            var hasher = new PasswordHasher<IdentityUser>();

            IdentityUser admin = new()
            {
                UserName = "admin@test.com",
                NormalizedUserName = "ADMIN@TEST.COM",
                Email = "admin@test.com",
                NormalizedEmail = "ADMIN@TEST.COM",
                EmailConfirmed = true
                //PasswordHash = hasher.HashPassword(null, "Password1!")
            };
            admin.PasswordHash = hasher.HashPassword(admin, "Password1!");

            //Seeding the User to AspNetUsers table
            builder.Entity<IdentityUser>().HasData(admin);

            ////Seeding the relation between our user and role to AspNetUserRoles table
            builder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    UserId = admin.Id,
                    RoleId = admin_role.Id
                }
            );
        }
        public DbSet<Shop.Models.CartItem> CartItem { get; set; } = default!;
        public DbSet<Shop.Models.Category> Category { get; set; } = default!;
    }
}
