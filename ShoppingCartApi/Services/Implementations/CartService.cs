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
    /// Initializes a new instance of the CartService class.
    /// </summary>
    /// <param name="cartRepository">The repository for performing cart operations.</param>
    /// <param name="mapper">The mapper for converting between entities and DTOs.</param>
    public async Task<IEnumerable<CartResponseDto>> AddToCart(CartRequestDto cartContents)
    {
      // Validate the quantity to ensure it is greater than zero.
      if (cartContents.Quantity <= 0)
      {
        throw new ArgumentException("Quantity must be greater than zero.");
      }

      // Add the item to the cart.
      await _cartRepository.AddToCart(cartContents);
      return await GetAllItems();
    }

    /// <summary>
    /// Retrieves all items currently in the cart.
    /// </summary>
    /// <returns>An enumerable collection of CartResponseDto objects representing the cart items.</returns>
    public async Task<IEnumerable<CartResponseDto>> GetAllItems()
    {
      // Retrieve all cart items from repository.
      var carts = await _cartRepository.GetAllItems();

      // Convert the cart entities to DTOs and return them.
      return carts.Select(cart => _mapper.Map<CartResponseDto>(cart));
    }
  }
}
