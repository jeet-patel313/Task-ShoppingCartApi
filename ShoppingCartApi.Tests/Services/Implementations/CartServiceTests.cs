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
  public class CartServiceTests
  {
    private readonly ICartService _cartService;
    private readonly Mock<ICartRepository> _cartRepositoryMock = new Mock<ICartRepository>();
    private readonly IMapper _mapper;

    public CartServiceTests()
    {
      var config = new MapperConfiguration(cfg => cfg.AddProfile(new ShoppingCartApi.Infrastructure.Mappers.CartProfile()));
      _mapper = config.CreateMapper();
      _cartService = new CartService(_cartRepositoryMock.Object, _mapper);
    }

    [Fact]
    public async Task AddToCart_ProductNotFound_ShouldThrowKeyNotFoundException()
    {
      // Arrange
      var cartContents = new CartRequestDto { ProductId = 1, Quantity = 1 };
      _cartRepositoryMock.Setup(repo => repo.AddToCart(It.IsAny<CartRequestDto>())).ThrowsAsync(new KeyNotFoundException());

      // Act & Assert
      await Assert.ThrowsAsync<KeyNotFoundException>(() => _cartService.AddToCart(cartContents));
    }

    [Fact]
    public async Task AddToCart_QuantityZero_ShouldThrowArgumentException()
    {
      // Arrange
      var cartContents = new CartRequestDto { ProductId = 1, Quantity = 0 };

      // Act & Assert
      await Assert.ThrowsAsync<ArgumentException>(() => _cartService.AddToCart(cartContents));
    }

    [Fact]
    public async Task AddToCart_ValidProduct_ShouldReturnUpdatedCart()
    {
      // Arrange
      var cartContents = new CartRequestDto { ProductId = 1, Quantity = 2 };
      var product = new Product { Id = 1, Name = "Sample Product", UnitPrice = 9.99M };
      var cart = new Cart { ProductId = 1, Product = product, Quantity = 2 };

      _cartRepositoryMock.Setup(repo => repo.AddToCart(It.IsAny<CartRequestDto>())).ReturnsAsync(cart);
      _cartRepositoryMock.Setup(repo => repo.GetAllItems()).ReturnsAsync(new List<Cart> { cart });

      // Act
      var result = await _cartService.AddToCart(cartContents);

      // Assert
      Assert.Single(result);
      Assert.Equal(cart.ProductId, result.First().ProductId);
    }

    [Fact]
    public async Task GetAllItems_ShouldReturnCartItems()
    {
      // Arrange
      var product = new Product { Id = 1, Name = "Sample Product", UnitPrice = 9.99M };
      var cart = new Cart { ProductId = 1, Product = product, Quantity = 2 };
      var cartList = new List<Cart> { cart };

      _cartRepositoryMock.Setup(repo => repo.GetAllItems()).ReturnsAsync(cartList);

      // Act
      var result = await _cartService.GetAllItems();

      // Assert
      Assert.Single(result);
      Assert.Equal(cart.ProductId, result.First().ProductId);
    }
  }
}
