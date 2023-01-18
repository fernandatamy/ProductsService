using AutoMapper;
using ProductsAPI.Data.Dtos;

namespace ProductsAPI.Profiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<CreateProductDTO, Product>();
            CreateMap<ReadProductDTO, Product>();
        }
    }
}
