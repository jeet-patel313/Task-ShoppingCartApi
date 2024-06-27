namespace ShoppingCartApi.Models.Dto.Carts
{
  public class CartRequestDto
  {
    // The unique identifier for the product to be added in the cart.
    public int ProductId { get; set; }
    // The quantity of the product to be added in the cart.
    public int Quantity { get; set; }
  }
}
