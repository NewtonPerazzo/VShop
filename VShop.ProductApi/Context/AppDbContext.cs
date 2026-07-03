using Microsoft.EntityFrameworkCore;
using VShop.ProductApi.Models;

namespace VShop.ProductApi.Context;

public class AppDbContext: DbContext
{
    // constructor
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }

    //FluentAPI
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //category
        modelBuilder.Entity<Category>().HasKey(c => c.CategoryId);
        modelBuilder.Entity<Category>().Property(c => c.Name).IsRequired().HasMaxLength(100);

        // product
        modelBuilder.Entity<Product>().HasKey(p => p.Id);
        modelBuilder.Entity<Product>().Property(p => p.Name).IsRequired().HasMaxLength(100);
        modelBuilder.Entity<Product>().Property(p => p.Description).IsRequired().HasMaxLength(255);
        modelBuilder.Entity<Product>().Property(p => p.Price).IsRequired().HasPrecision(12, 2);
        modelBuilder.Entity<Product>().Property(p => p.ImageUrl).IsRequired().HasMaxLength(255);
        modelBuilder.Entity<Category>().HasMany(g => g.Products)
            .WithOne(g => g.Category).IsRequired()
            .OnDelete(DeleteBehavior.Cascade); //associated products will be deleted when a category is deleted

        modelBuilder.Entity<Category>().HasData(
            new Category { CategoryId = 1, Name = "Electronics" },
            new Category { CategoryId = 2, Name = "Clothes" }
          );
    }
}
