using AutoMapper;
using ShoppingCartApi.Models.Dto.Carts;
using ShoppingCartApi.Repositories.Interfaces;
using ShoppingCartApi.Services.Interfaces;

namespace ShoppingCartApi.Services.Implementations
{
  public class CartService : ICartService
  {
    private readonly ICartRepository _cartRepository;
    private readonly IMapper _mapper;

    // Constructor to initialize the service with the given repository and mapper.
    public CartService(ICartRepository cartRepository, IMapper mapper)
    {
      _cartRepository = cartRepository;
      _mapper = mapper;
    }

    /// <summary>
    /// Adds an item to the cart based on the provided cart contents.
    /// Validates the quantity to ensure it is greater than zero.
    /// </summary>
    /// <param name="cartContents">The contents of the cart item to add.</param>
    /// <returns>An enumerable collection of CartResponseDto representing the updated cart items.</returns>
    public async Task<IEnumerable<CartResponseDto>> AddToCart(CartRequestDto cartContents)
    {
      if (cartContents.Quantity <= 0)
      {
        throw new ArgumentException("Quantity must be greater than zero.");
      }

      await _cartRepository.AddToCart(cartContents);
      return await GetAllItems();
    }

    /// <summary>
    /// Retrieves all items currently in the cart.
    /// </summary>
    /// <returns>An enumerable collection of CartResponseDto representing the cart items.</returns>
    public async Task<IEnumerable<CartResponseDto>> GetAllItems()
    {
      var carts = await _cartRepository.GetAllItems();
      return carts.Select(cart => _mapper.Map<CartResponseDto>(cart));
    }
  }
}
