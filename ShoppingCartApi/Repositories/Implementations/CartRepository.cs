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
      // Find product in the database based on productId.
      var product = await _context.Products.FindAsync(cartContents.ProductId);
      if (product == null)
      {
        throw new KeyNotFoundException("Product not found.");
      }

      // Find cart item in the database based on productId.
      var cart = await _context.Carts.FindAsync(cartContents.ProductId);
      if (cart == null)
      {
        // If the cart item does not exist, create a new cart item.
        cart = new Cart
        {
          ProductId = cartContents.ProductId,
          Quantity = cartContents.Quantity
        };
        _context.Carts.Add(cart); // Add the new cart item.
      }
      else
      {
        // If the cart item already exists, update the quantity.
        cart.Quantity += cartContents.Quantity;
        _context.Carts.Update(cart); // Update the existing cart item.
      }

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
      return await _context.Carts.Include(c => c.Product).ToListAsync();
    }
  }
}
