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
            _connection = new ConnectionFactory() {Uri = new Uri("amqps://yyyqxodl:QJ8qUbcyj-2np3fvICCpp8IwTrFwGD0S@jackal.rmq.cloudamqp.com/yyyqxodl")}.CreateConnection();
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
