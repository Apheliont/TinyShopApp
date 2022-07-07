using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyShop.Catalog.Entities;

namespace TinyShop.Catalog
{
    public class AppDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CategoryFilter> CategoryFilters { get; set; }
        public DbSet<CategoryFilterTranslation> CategoryFilterTranslations { get; set; }
        public DbSet<CategoryTranslation> CategoryTranslations { get; set; }
        public DbSet<ProductTranslation> ProductTranslations { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<Country> Countries { get; set; }

        public AppDbContext() { }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseNpgsql("Host=localhost;Database=TinyShop.Catalog;Username=postgres;Password=mYAwesomePassw0rd")
                .UseSnakeCaseNamingConvention();
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().Property(p => p.CreatedAt).HasDefaultValueSql("CURRENT_DATE");
            modelBuilder.Entity<Product>().Property(p => p.UpdatedAt).HasDefaultValueSql("CURRENT_DATE");
            modelBuilder.Entity<Category>().Property(p => p.CreatedAt).HasDefaultValueSql("CURRENT_DATE");
            modelBuilder.Entity<Category>().Property(p => p.UpdatedAt).HasDefaultValueSql("CURRENT_DATE");
            modelBuilder.Entity<CategoryFilter>().Property(p => p.Index).HasDefaultValue(0);
            modelBuilder.Entity<Category>().HasMany(c => c.Products).WithOne(p => p.Category);

            modelBuilder.Entity<Category>()
                .HasMany(c => c.SubCategories)
                .WithOne(c => c.ParentNode)
                .HasForeignKey(c => c.ParentId);

            modelBuilder.Entity<Product>()
                .HasMany(c => c.Images)
                .WithMany(p => p.Products)
                .UsingEntity<ProductsImages>(
                    p => p.HasOne(j => j.Image).WithMany(m => m.ProductsImages).HasForeignKey(pt => pt.ImageId),
                    p => p.HasOne(j => j.Product).WithMany(m => m.ProductsImages).HasForeignKey(pt => pt.ProductId),
                    p => p.HasIndex(u => new { u.ImageId, u.ProductId }).IsUnique()
                );

            modelBuilder.Entity<ProductTranslation>().HasKey(pt => new { pt.ProductId, pt.LanguageCode });
            modelBuilder.Entity<CategoryFilterTranslation>().HasKey(cft => new { cft.CategoryFilterId, cft.LanguageCode });
            modelBuilder.Entity<CategoryTranslation>().HasKey(ct => new { ct.CategoryId, ct.LanguageCode });
        }
    }
}
