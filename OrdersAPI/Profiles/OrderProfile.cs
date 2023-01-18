using AutoMapper;
using OrdersAPI.Data.Dtos;

namespace OrdersAPI.Profiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<CreateOrderDTO, Order>();
        }
    }
}
