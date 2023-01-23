using ProductsAPI.Data.Dtos;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace ProductsAPI.RabbitMqClient
{
    public class RabbitMqClient : IRabbitMqClient
    {

        private readonly IConfiguration _configuration;
        private readonly IConnection _connection;
        private readonly IModel _channel;

        public RabbitMqClient(IConfiguration configuration)
        {
            _configuration = configuration;
            ConnectionFactory factory = new ConnectionFactory();
            factory.UserName = _configuration["RabbitMqUserName"];
            factory.Password = _configuration["RabbitMqPassword"];
            factory.VirtualHost = _configuration["RabbitMqVirtualHost"];
            factory.HostName = _configuration["RabbitMqHostName"];
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.ExchangeDeclare(exchange: "trigger", type: ExchangeType.Fanout);
        }
        public void CreateManyProducts(CreateProductDTO dto)
        {
            string mensagem = JsonSerializer.Serialize(dto);
            var body = Encoding.UTF8.GetBytes(mensagem);

            _channel.BasicPublish(exchange: "trigger",
                routingKey: "",
                basicProperties: null,
                body: body
                );
        }
    }
}
