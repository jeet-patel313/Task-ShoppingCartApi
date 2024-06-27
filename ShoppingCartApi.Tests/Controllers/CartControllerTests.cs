using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ShoppingCartApi.Controllers;
using ShoppingCartApi.Models.Dto.Carts;
using ShoppingCartApi.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace ShoppingCartApi.Tests.Controllers
{
  public class CartControllerTests
  {
    private readonly CartController _cartController;
    private readonly Mock<ICartService> _cartServiceMock = new Mock<ICartService>();
    private readonly IMapper _mapper;

    public CartControllerTests()
    {
      var config = new MapperConfiguration(cfg => cfg.AddProfile(new ShoppingCartApi.Infrastructure.Mappers.CartProfile()));
      _mapper = config.CreateMapper();
      _cartController = new CartController(_cartServiceMock.Object, _mapper);
    }

    [Fact]
    public async Task AddToCart_ProductNotFound_ShouldReturnNotFound()
    {
      // Arrange
      var cartContents = new CartRequestDto { ProductId = 1, Quantity = 1 };
      _cartServiceMock.Setup(service => service.AddToCart(It.IsAny<CartRequestDto>())).ThrowsAsync(new KeyNotFoundException());

      // Act
      var result = await _cartController.AddToCart(cartContents);

      // Assert
      var actionResult = Assert.IsType<NotFoundObjectResult>(result);
      Assert.Equal(404, actionResult.StatusCode);
    }

    [Fact]
    public async Task AddToCart_QuantityZero_ShouldReturnBadRequest()
    {
      // Arrange
      var cartContents = new CartRequestDto { ProductId = 1, Quantity = 0 };

      // Act
      var result = await _cartController.AddToCart(cartContents);

      // Assert
      var actionResult = Assert.IsType<BadRequestObjectResult>(result);
      Assert.Equal(400, actionResult.StatusCode);
    }

    [Fact]
    public async Task AddToCart_ValidProduct_ShouldReturnOk()
    {
      // Arrange
      var cartContents = new CartRequestDto { ProductId = 1, Quantity = 2 };
      var cartResponse = new List<CartResponseDto>
            {
                new CartResponseDto { ProductId = 1, ProductName = "Sample Product", ProductUnitPrice = 9.99M, Quantity = 2 }
            };

      _cartServiceMock.Setup(service => service.AddToCart(It.IsAny<CartRequestDto>())).ReturnsAsync(cartResponse);

      // Act
      var result = await _cartController.AddToCart(cartContents);

      // Assert
      var actionResult = Assert.IsType<OkObjectResult>(result);
      var returnValue = Assert.IsType<List<CartResponseDto>>(actionResult.Value);
      Assert.Single(returnValue);
      Assert.Equal(cartResponse.First().ProductId, returnValue.First().ProductId);  // Ensure System.Linq is used
    }

    [Fact]
    public async Task GetAllItems_ShouldReturnOk()
    {
      // Arrange
      var cartResponse = new List<CartResponseDto>
            {
                new CartResponseDto { ProductId = 1, ProductName = "Sample Product", ProductUnitPrice = 9.99M, Quantity = 2 }
            };

      _cartServiceMock.Setup(service => service.GetAllItems()).ReturnsAsync(cartResponse);

      // Act
      var result = await _cartController.GetAllItems();

      // Assert
      var actionResult = Assert.IsType<OkObjectResult>(result);
      var returnValue = Assert.IsType<List<CartResponseDto>>(actionResult.Value);
      Assert.Single(returnValue);
      Assert.Equal(cartResponse.First().ProductId, returnValue.First().ProductId);  // Ensure System.Linq is used
    }
  }
}
