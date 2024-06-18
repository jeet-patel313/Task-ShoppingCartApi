using Microsoft.EntityFrameworkCore;
using ShoppingCartApi.Contexts;
using ShoppingCartApi.Models.Data;
using ShoppingCartApi.Models.Dto.Carts;
using ShoppingCartApi.Repositories.Interfaces;

namespace ShoppingCartApi.Repositories.Implementation
{
  public class CartRepository : ICartRepository
  {
    private readonly ApplicationDBContext _context;

    /// <summary>
    /// Initializes a new instance of the CartRepository class.
    /// </summary>
    /// <param name="context">The database context used to interact with database.</param>
    public CartRepository(ApplicationDBContext context)
    {
      _context = context;
    }

    /// <summary>
    /// Adds an item to the cart based on the provided cart contents.
    /// </summary>
    /// <param name="cartContents">The contents of the cart item to add.</param>
    /// <returns>The updated cart item.</returns>
    public async Task<Cart> AddToCart(CartRequestDto cartContents)
    {
      // Create a new cart item.
      var cart = new Cart
      {
        Name = cartContents.Name,
        Price = cartContents.Price,
        Quantity = cartContents.Quantity
      };

      // Add the new cart item.
      _context.Carts.Add(cart);

      // Save changes to the database.
      await _context.SaveChangesAsync();
      return cart;
    }

    /// <summary>
    /// Retrieves all items currently in the cart.
    /// </summary>
    /// <returns>A collection of Cart objects.</returns>
    public async Task<IEnumerable<Cart>> GetAllItems()
    {
      return await _context.Carts.ToListAsync();
    }
  }
}
