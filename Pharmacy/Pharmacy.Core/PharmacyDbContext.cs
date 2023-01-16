using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Pharmacy.Core;

namespace Pharmacy.Core
{
    public class PharmacyDbContext : IdentityDbContext<User>
    {
        public PharmacyDbContext(DbContextOptions<PharmacyDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Seed();
            builder.Seed1();
            base.OnModelCreating(builder);
        }
        
        public DbSet<Category> Category { get; set; }
        public DbSet<SubCategory> SubCategory { get; set; }
        public DbSet<Medicaments> Medicaments { get; set; }
        public DbSet<SubCategoryMedicaments> SubCategoryMedicaments { get; set; }
        public DbSet<Brend> Brend { get; set; }
        public DbSet<Country> Country { get; set; }
        public DbSet<Catalog> Catalog { get; set; }
        public DbSet<ProductLine> ProductLine { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }
        public DbSet<OrderItems> OrderItems { get; set; }
        public DbSet<OrderAddress> OrderAddress { get; set; }
    }
}