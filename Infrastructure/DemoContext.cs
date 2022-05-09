using DemoApp.Infrastructure.Products;
using Microsoft.EntityFrameworkCore;

namespace DemoApp.Infrastructure
{
    public class DemoContext : DbContext
    {
        public DbSet<ProductEntity> Products { get; set; }

        public DemoContext(DbContextOptions<DemoContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Makes all queries non-tracking by default
            optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        }

        protected override void OnModelCreating(ModelBuilder modelBuider)
        {
            modelBuider.Entity<ProductEntity>().HasData(
                new ProductEntity { Id = 1, Name = "GeForce RTX 3080", Price = 9449 },
                new ProductEntity { Id = 2, Name = "980 PRO PCle 4.0 NVMe", Price = 2239 },
                new ProductEntity { Id = 3, Name = "Intel Core i9-10900K", Price = 5490 },
                new ProductEntity { Id = 4, Name = "Fractal DesignDefine R5", Price = 1396 },
                new ProductEntity { Id = 5, Name = "Kingston Fury Beast DDR5", Price = 2645 });
        }
    }
}