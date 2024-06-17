using AutoMapper;
using ShoppingCartApi.Models.Data;
using ShoppingCartApi.Models.Dto.Carts;

namespace ShoppingCartApi.Infrastructure.Mappers
{
  public class CartProfile : Profile
  {
    /// <summary>
    /// Initializes a new instance of the CartProfile class.
    /// Configures the mappings between Cart and CartResponseDto.
    /// </summary>
    public CartProfile()
    {
      // Configures mappings between Cart and CartResponseDto.
      CreateMap<Cart, CartResponseDto>()
          .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name))
          .ForMember(dest => dest.ProductUnitPrice, opt => opt.MapFrom(src => src.Product.UnitPrice))
          .ReverseMap();
    }
  }
}
