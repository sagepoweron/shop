using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Shop.Models;

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

            builder.Entity<Product>().HasData(
                new Product()
                {
                    Name = "Bicycle",
                    Brand = "Test",
                    ListPrice = 100,
                    SalePrice = 90
                },
                new Product()
                {
                    Name = "Book",
                    Brand = "Test",
                    ListPrice = 10,
                    SalePrice = 9
                },
                new Product()
                {
                    Name = "Lamp",
                    Brand = "Other",
                    ListPrice = 30,
                    SalePrice = 20
                }

                );

            builder.Entity<IdentityRole>().HasData(
                new IdentityRole(Helpers.Admin_Role),
                new IdentityRole(Helpers.Customer_Role)
                );
        }
        public DbSet<Shop.Models.CartItem> CartItem { get; set; } = default!;
    }
}
