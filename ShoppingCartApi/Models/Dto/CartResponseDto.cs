namespace ShoppingCartApi.Models.Dto.Carts
{
  public class CartResponseDto
  {
    // The unique identifier for the product in the cart.
    public int ProductId { get; set; }

    // The name of the product in the cart.
    public string ProductName { get; set; } = string.Empty;

    // The unit price of the product in the cart.
    public decimal ProductUnitPrice { get; set; }

    // The quantity of the product in the cart.
    public int Quantity { get; set; }
  }
}