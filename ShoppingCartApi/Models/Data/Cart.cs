using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoppingCartApi.Models.Data
{
  public class Cart
  {
    [Key]
    [ForeignKey("Product")]
    public int ProductId { get; set; }

    public Product Product { get; set; } = new Product();

    [Range(1, int.MaxValue, ErrorMessage = "Quantity must be greater than zero.")]
    public int Quantity { get; set; }
  }
}
