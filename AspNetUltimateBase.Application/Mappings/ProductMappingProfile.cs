using AutoMapper;
using AspNetUltimateBase.Application.Dtos;
using AspNetUltimateBase.Domain.Entities;

namespace AspNetUltimateBase.Application.Mappings;

public class ProductMappingProfile : Profile
{
    public ProductMappingProfile()
    {
        CreateMap<ProductEntity, ProductDto>()
            .ForMember(dest => dest.Barcode, opt => opt.MapFrom(src => src.Barcode))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.Weight, opt => opt.MapFrom(src => src.ProductDetails.Weight))
            .ForMember(dest => dest.Energy, opt => opt.MapFrom(src => src.ProductDetails.Energy))
            .ForMember(dest => dest.Protein, opt => opt.MapFrom(src => src.ProductDetails.Protein))
            .ForMember(dest => dest.Fat, opt => opt.MapFrom(src => src.ProductDetails.Fat))
            .ForMember(dest => dest.Carbohydrates, opt => opt.MapFrom(src => src.ProductDetails.Carbohydrates))
            .ForMember(dest => dest.Sugar, opt => opt.MapFrom(src => src.ProductDetails.Sugar))
            .ForMember(dest => dest.Salt, opt => opt.MapFrom(src => src.ProductDetails.Salt))
            .ForMember(dest => dest.Fiber, opt => opt.MapFrom(src => src.ProductDetails.Fiber))
            .ReverseMap();
    }
}
