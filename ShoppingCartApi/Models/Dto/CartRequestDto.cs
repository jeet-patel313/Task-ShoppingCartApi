namespace ShoppingCartApi.Models.Dto.Carts
{
  public class CartRequestDto
  {
    // The name of the product to be added in the cart.
    public string Name { get; set; } = string.Empty;

    // The price of the product to be added in the cart.
    public decimal Price { get; set; }

    // The quantity of the product to be added in the cart.
    public int Quantity { get; set; }
  }
}
