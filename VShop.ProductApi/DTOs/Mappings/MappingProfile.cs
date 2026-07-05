using AutoMapper;
using VShop.ProductApi.Models;

namespace VShop.ProductApi.DTOs.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile() {
            CreateMap<Category, CategoryDTO>().ReverseMap();
            CreateMap<ProductDTO, Product>();

            //get the category name from the category object and map it to the CategoryName property of the ProductDTO
            CreateMap<Product, ProductDTO>()
                .ForMember(x => x.CategoryName, opt => opt.MapFrom(src => src.Category.Name));
    }
    }
}
