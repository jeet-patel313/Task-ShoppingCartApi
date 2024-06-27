using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoppingCartApi.Models.Data
{
  public class Product
  {
    public int Id { get; set; }

    [Required]
    public string Name { get; set; } = string.Empty;

    [Column(TypeName = "decimal(18, 2)")]
    [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than zero.")]
    public decimal UnitPrice { get; set; }
  }
}
