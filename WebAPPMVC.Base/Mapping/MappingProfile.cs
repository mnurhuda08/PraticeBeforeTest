using AutoMapper;
using WebAPPMVC.Base.Models;
using WebAPPMVC.Base.Models.DTOs.CategoryDTO;
using WebAPPMVC.Base.Models.DTOs.ProductDTO;

namespace WebAPPMVC.Base.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Category, CategoryDTOs>().ReverseMap();
            CreateMap<Product,ProductDTOs>().ReverseMap();
        }
    }
}
