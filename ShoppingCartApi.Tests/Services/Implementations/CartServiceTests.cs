using AutoMapper;
using Moq;
using ShoppingCartApi.Models.Data;
using ShoppingCartApi.Models.Dto.Carts;
using ShoppingCartApi.Repositories.Interfaces;
using ShoppingCartApi.Services.Implementations;
using ShoppingCartApi.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace ShoppingCartApi.Tests.Services
{
  /// <summary>
  /// Unit tests for CartService.
  /// </summary>
  public class CartServiceTests
  {
    private readonly ICartService _cartService;
    private readonly Mock<ICartRepository> _cartRepositoryMock = new Mock<ICartRepository>();
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of the <see cref="CartServiceTests"/> class.
    /// </summary>
    public CartServiceTests()
    {
      var config = new MapperConfiguration(cfg => cfg.AddProfile(new ShoppingCartApi.Infrastructure.Mappers.CartProfile()));
      _mapper = config.CreateMapper();
      _cartService = new CartService(_cartRepositoryMock.Object, _mapper);
    }

    /// <summary>
    /// Test for AddToCart method when product is not found.
    /// </summary>
    [Fact]
    public async Task AddToCart_ProductNotFound_ShouldThrowKeyNotFoundException()
    {
      // Arrange
      var cartContents = new CartRequestDto { Name = "Non-existent Product", Price = 9.99M, Quantity = 1 };
      _cartRepositoryMock.Setup(repo => repo.AddToCart(It.IsAny<CartRequestDto>())).ThrowsAsync(new KeyNotFoundException());

      // Act & Assert
      await Assert.ThrowsAsync<KeyNotFoundException>(() => _cartService.AddToCart(cartContents));
    }

    /// <summary>
    /// Test for AddToCart method when quantity is zero.
    /// </summary>
    [Fact]
    public async Task AddToCart_QuantityZero_ShouldThrowArgumentException()
    {
      // Arrange
      var cartContents = new CartRequestDto { Name = "Sample Product", Price = 9.99M, Quantity = 0 };

      // Act & Assert
      await Assert.ThrowsAsync<ArgumentException>(() => _cartService.AddToCart(cartContents));
    }

    /// <summary>
    /// Test for AddToCart method when valid product is added.
    /// </summary>
    [Fact]
    public async Task AddToCart_ValidProduct_ShouldReturnUpdatedCart()
    {
      // Arrange
      var cartContents = new CartRequestDto { Name = "Sample Product", Price = 9.99M, Quantity = 2 };
      var cart = new Cart { Name = "Sample Product", Price = 9.99M, Quantity = 2 };

      _cartRepositoryMock.Setup(repo => repo.AddToCart(It.IsAny<CartRequestDto>())).ReturnsAsync(cart);
      _cartRepositoryMock.Setup(repo => repo.GetAllItems()).ReturnsAsync(new List<Cart> { cart });

      // Act
      var result = await _cartService.AddToCart(cartContents);

      // Assert
      Assert.Single(result);
      Assert.Equal(cart.Name, result.First().Name);
    }

    /// <summary>
    /// Test for GetAllItems method.
    /// </summary>
    [Fact]
    public async Task GetAllItems_ShouldReturnCartItems()
    {
      // Arrange
      var cart = new Cart { Name = "Sample Product", Price = 9.99M, Quantity = 2 };
      var cartList = new List<Cart> { cart };

      _cartRepositoryMock.Setup(repo => repo.GetAllItems()).ReturnsAsync(cartList);

      // Act
      var result = await _cartService.GetAllItems();

      // Assert
      Assert.Single(result);
      Assert.Equal(cart.Name, result.First().Name);
    }
  }
}
