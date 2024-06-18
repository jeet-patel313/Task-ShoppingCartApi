using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ShoppingCartApi.Models.Dto.Carts;
using ShoppingCartApi.Services.Interfaces;

namespace ShoppingCartApi.Controllers
{
  /// <summary>
  /// Controller for handling cart operations.
  /// </summary>
  [Route("api/carts")]
  [Produces("application/json")]
  [ApiController]
  public class CartController : ControllerBase
  {
    private readonly ICartService _cartService;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of the class.
    /// </summary>
    /// <param name="cartService">The service for performing cart operations.</param>
    /// <param name="mapper">The mapper for converting between entities and DTOs.</param>
    public CartController(ICartService cartService, IMapper mapper)
    {
      _cartService = cartService;
      _mapper = mapper;
    }

    /// <summary>
    /// Adds an item to the cart.
    /// </summary>
    /// <param name="cartContents">The contents of the cart item to add.</param>
    /// <returns>
    /// A response with the updated cart with all items if successful; otherwise, an appropriate error response.
    /// </returns>
    /// <response code="200">Returns the updated cart with all items.</response>
    /// <response code="400">If the quantity is less than or equal to zero.</response>
    [HttpPost]
    public async Task<IActionResult> AddToCart([FromBody] CartRequestDto cartContents)
    {
      // Validate the quantity to ensure it is greater than zero.
      if (cartContents.Quantity <= 0)
      {
        return BadRequest("Quantity must be greater than zero.");
      }

      try
      {
        var carts = await _cartService.AddToCart(cartContents);
        return Ok(carts);
      }
      catch (ArgumentException ex)
      {
        return BadRequest(ex.Message);
      }
    }

    /// <summary>
    /// Retrieves all items in the cart.
    /// </summary>
    /// <returns>A response with all cart items.</returns>
    /// <response code="200">Returns all cart items.</response>
    [HttpGet]
    public async Task<IActionResult> GetAllItems()
    {
      var carts = await _cartService.GetAllItems();
      return Ok(carts);
    }
  }
}
