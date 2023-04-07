using Microsoft.EntityFrameworkCore;
using AdventureWork.Models;

namespace AdventureWork.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<Product> Product { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<ProductInventory> ProductInventories { get; set; }
        public DbSet<ProductPhoto> ProductPhoto { get; set; }
        public DbSet<ProductSubcategory> ProductSubcategories { get; set; }
        public DbSet<ProductProductPhoto> ProductProductPhoto { get; set; }

        //   protected override void OnModelCreating(ModelBuilder modelBuilder)
        //  {
        //      modelBuilder.Entity<ProductProductPhoto>().HasKey(p => new { p.ProductID, p.ProductPhotoID });
        //     modelBuilder.Entity<ProductProductPhoto>()
        //          .HasOne(pc => pc.ProductID)
        // }
    }
}
