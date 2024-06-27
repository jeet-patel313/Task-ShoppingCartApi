using ShoppingCartApi.Models.Dto.Carts;

namespace ShoppingCartApi.Services.Interfaces
{
  public interface ICartService
  {
    /// <summary>
    /// Adds an item to cart based on the provided cart contents.
    /// </summary>
    /// <param name="cartContents">The contents of the cart item to add.</param>
    /// <returns>
    /// A task that represents the async operation and result contains an enumerable collection of CartResponseDto.
    /// </returns>
    Task<IEnumerable<CartResponseDto>> AddToCart(CartRequestDto cartContents);

    /// <summary>
    /// Retrieve all items in the cart.
    /// </summary>
    /// <returns>
    /// A task that represents the async operation and result contains an enumerable collection of CartResponseDto.
    /// </returns>
    Task<IEnumerable<CartResponseDto>> GetAllItems();
  }
}
