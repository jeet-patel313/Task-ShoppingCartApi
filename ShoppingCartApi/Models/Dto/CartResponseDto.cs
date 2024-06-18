namespace ShoppingCartApi.Models.Dto.Carts
{
  public class CartResponseDto
  {
    // The unique identifier for the product in the cart.
    public int Id { get; set; }

    // The name of the product in the cart.
    public string Name { get; set; } = string.Empty;

    // The unit price of the product in the cart.
    public decimal Price { get; set; }

    // The quantity of the product in the cart.
    public int Quantity { get; set; }
  }
}
