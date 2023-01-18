using ProductsAPI.Data.Dtos;

namespace ProductsAPI.ServiceHttpClient
{
    public interface IProductServiceHttpClient
    {
        public void SendOrderToProductService(ReadProductDTO readDto);
    }
}