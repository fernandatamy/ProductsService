using OrdersAPI.Data.Dtos;

namespace OrdersAPI.ServiceHttpClient
{
    public interface IProductServiceHttpClient
    {
        public void SendOrderToProductService(ReadOrderDTO readDto);
    }
}