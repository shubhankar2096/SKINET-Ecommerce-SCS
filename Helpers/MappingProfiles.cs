using AutoMapper;
using Core.Entities;
using SKINET_Ecommerce.DTOs;

namespace SKINET_Ecommerce.Helpers
{
    //Automapper between Entitities and DTOs
    public class MappingProfiles : Profile
    {
        public MappingProfiles() 
        {
            CreateMap<Product, ProductToReturnDTO>()
                .ForMember(destination => destination.ProductBrand, o => o.MapFrom(source => source.ProductBrand.Name))
                .ForMember(destination => destination.ProductType, o => o.MapFrom(source => source.ProductType.Name))
                .ForMember(destination => destination.PictureURL, o => o.MapFrom<ProductURLResolver>());
        }
    }
}
