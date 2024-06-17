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
    }
  }
}
