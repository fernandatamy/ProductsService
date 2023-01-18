using ProductsAPI.EventProcessor;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace ProductsAPI.RabbitMqSubscriber
{
    public class RabbitMqSubscriber : BackgroundService
    {
        private readonly IConfiguration _configuration;
        private readonly string _queueName;
        private readonly IConnection _connection;
        private IModel _channel;
        private IEventProcessor _eventProcessor;

        public RabbitMqSubscriber(IConfiguration configuration, IEventProcessor eventProcessor)
        {
            _configuration = configuration;
            _connection = new ConnectionFactory() {Uri = new Uri("amqps://yyyqxodl:QJ8qUbcyj-2np3fvICCpp8IwTrFwGD0S@jackal.rmq.cloudamqp.com/yyyqxodl")}.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.ExchangeDeclare(exchange: "trigger", type: ExchangeType.Fanout);
            _queueName = _channel.QueueDeclare().QueueName;
            _channel.QueueBind(queue: _queueName, exchange: "trigger", routingKey: "");
            _eventProcessor = eventProcessor;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            EventingBasicConsumer? consumer = new EventingBasicConsumer(_channel);

            consumer.Received += (ModuleHandle, ea) =>
            {
                ReadOnlyMemory<byte> body = ea.Body;
                string? message = System.Text.Encoding.UTF8.GetString(body.ToArray());
                _eventProcessor.Process(message);
            };
            _channel.BasicConsume(queue: _queueName, autoAck: true, consumer: consumer);
            return Task.CompletedTask;
        }

    }
}
