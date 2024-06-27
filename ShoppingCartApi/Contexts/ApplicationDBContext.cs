using Microsoft.EntityFrameworkCore;
using ShoppingCartApi.Models.Data;

namespace ShoppingCartApi.Contexts
{
  public class ApplicationDBContext : DbContext
  {
    public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options) { }

    public DbSet<Cart> Carts { get; set; }
    public DbSet<Product> Products { get; set; }

    // Configures the entity mappings and relationships.
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<Cart>()
          .HasKey(c => c.ProductId);

      // Predefined 3 products in the Product table
      modelBuilder.Entity<Product>().HasData(
        new Product { Id = 1, Name = "Philips Smart Electric Toothbrush", UnitPrice = 20.99M },
        new Product { Id = 2, Name = "Philips Norelco Shaver 9000", UnitPrice = 30.99M },
        new Product { Id = 3, Name = "Philips OLED+959 TV", UnitPrice = 120.99M }
      );
    }
  }
}
