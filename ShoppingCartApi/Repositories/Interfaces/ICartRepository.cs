using ShoppingCartApi.Models.Data;
using ShoppingCartApi.Models.Dto.Carts;

namespace ShoppingCartApi.Repositories.Interfaces
{
  public interface ICartRepository
  {
    /// <summary>
    /// Adds an item to cart based on the provided cart contents.
    /// </summary>
    /// <param name="cartContents">The contents of the cart item to add.</param>
    /// <returns>The updated object.</returns>
    Task<Cart> AddToCart(CartRequestDto cartContents);

    /// <summary>
    /// Retrieves all items currently in the cart.
    /// </summary>
    /// <returns>An enumerable collection of objects.</returns>
    Task<IEnumerable<Cart>> GetAllItems();
  }
}
