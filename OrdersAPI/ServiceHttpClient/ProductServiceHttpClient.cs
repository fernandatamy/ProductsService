using OrdersAPI.Data.Dtos;
using System.Text;
using System.Text.Json;

namespace OrdersAPI.ServiceHttpClient
{
    public class ProductServiceHttpClient : IProductServiceHttpClient
    {
        private readonly HttpClient _client;
        private readonly IConfiguration _configuration;

        public ProductServiceHttpClient(HttpClient client, IConfiguration configuration)
        {
            _client = client;
            _configuration = configuration;
        }

        public void SendOrderToProductService(ReadOrderDTO readDto)
        {
            var conteudoHttp = new StringContent
                (
                    JsonSerializer.Serialize(readDto),
                    Encoding.UTF8,
                    "application/json"
                );

            await _client.PostAsync(_configuration["ProductsAPI"], conteudoHttp);
        }
    }
}
