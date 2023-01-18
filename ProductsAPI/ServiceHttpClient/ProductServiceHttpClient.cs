using ProductsAPI.Data.Dtos;
using System;
using System.Text;
using System.Text.Json;

namespace ProductsAPI.ServiceHttpClient
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

        public async void SendOrderToProductService(ReadProductDTO readDto)
        {           
            var conteudoHttp = new StringContent
                (
                    JsonSerializer.Serialize(readDto),
                    Encoding.UTF8,
                    "application/json"
                );
            await _client.PostAsync(_configuration["ProductService"], conteudoHttp);
        }
    }
}
