using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using ProjetoDeVendasComCQRS.Domain.Commands.Pedido;
using ProjetoDeVendasComCQRS.Domain.Interfaces.Publisher;
using ProjetoDeVendasComCQRS.Domain.Models;
using RabbitMQ.Client;
using System.Text;

namespace ProjetoDeVendasComCQRS.Infra.Publisher
{
    public class AdicionarPedidoPublisher : IAdicionarPedidoPublisher
    {
        private readonly ConnectionFactory _connectionFactory;
        private readonly RabbitMqConfiguration _configuration;
        public AdicionarPedidoPublisher(IOptions<RabbitMqConfiguration> options)
        {
            _configuration = options.Value;
            _connectionFactory = new ConnectionFactory
            {
                UserName = _configuration.UserName,
                Password = _configuration.Password,
                VirtualHost = _configuration.VirtualHost,
                HostName = _configuration.Host
            };
        }

        public void Publisher(AdicionarPedidoCommand command)
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(
                        queue: _configuration.Queue,
                        durable: true,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null);

                    var stringMessage = JsonConvert.SerializeObject(command);
                    var bytesMessage = Encoding.UTF8.GetBytes(stringMessage);
                    var properties = channel.CreateBasicProperties();
                    properties.Persistent = true;
                    channel.BasicPublish(
                        exchange: "",
                        routingKey: _configuration.Queue,
                        basicProperties: properties,
                        body: bytesMessage);
                }
            }
        }
    }
}
