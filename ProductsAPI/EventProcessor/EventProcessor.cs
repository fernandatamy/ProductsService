using AutoMapper;
using ProductsAPI.Data.Dtos;
using ProductsAPI.Data.Repository;
using System.Text.Json;

namespace ProductsAPI.EventProcessor
{
    public class EventProcessor : IEventProcessor
    {

        private readonly IMapper _mapper;
        private readonly IServiceScopeFactory _scopeFactory;

        public EventProcessor(IMapper mapper, IServiceScopeFactory scopeFactory)
        {
            _mapper = mapper;
            _scopeFactory = scopeFactory;
        }
        public void Process(string message)
        {
            using var scope = _scopeFactory.CreateScope();
            var productRepository = scope.ServiceProvider.GetRequiredService<IProductRepository>();
            var productDto = JsonSerializer.Deserialize<ReadProductDTO>(message);
            var product = _mapper.Map<Product>(productDto);

            if (!productRepository.ProductExists(product.Id))
            {
                productRepository.CreateProduct(product);
                productRepository.SaveChanges();
            }
        }
              
    }
}
