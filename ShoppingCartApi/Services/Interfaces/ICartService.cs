using ShoppingCartApi.Models.Dto.Carts;

namespace ShoppingCartApi.Services.Interfaces
{
  public interface ICartService
  {
    /// <summary>
    /// Adds an item to the cart based on the provided cart contents.
    /// </summary>
    /// <param name="cartContents">The contents of the cart item to add.</param>
    /// <returns>A task that represents the asynchronous operation, which, when completed, contains an enumerable collection of CartResponseDto.</returns>
    Task<IEnumerable<CartResponseDto>> AddToCart(CartRequestDto cartContents);

    /// <summary>
    /// Retrieves all items currently in the cart.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation, which, when completed, contains an enumerable collection of CartResponseDto.</returns>
    Task<IEnumerable<CartResponseDto>> GetAllItems();
  }
}
