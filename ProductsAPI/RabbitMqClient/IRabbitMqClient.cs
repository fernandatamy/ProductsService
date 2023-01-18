using ProductsAPI.Data.Dtos;

namespace ProductsAPI.RabbitMqClient
{
    public interface IRabbitMqClient
    {
        public void CreateManyProducts(CreateProductDTO dto);
    }
}
