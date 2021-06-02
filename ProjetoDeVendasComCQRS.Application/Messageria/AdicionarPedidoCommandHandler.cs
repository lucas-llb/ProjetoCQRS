using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using ProjetoDeVendasComCQRS.Application.Interfaces.Mappers;
using ProjetoDeVendasComCQRS.Domain.Commands.Pedido;
using ProjetoDeVendasComCQRS.Domain.Interfaces.Repository;
using ProjetoDeVendasComCQRS.Domain.Models;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProjetoDeVendasComCQRS.Application.Messageria
{
    public class AdicionarPedidoCommandHandler : BackgroundService
    {
        private readonly RabbitMqConfiguration _configuration;
        private readonly IPedidoDocumentMapper _pedidoDocumentMapper;
        private readonly IPedidoMongoRepository _repository;
        private readonly ConnectionFactory _factory;
        public AdicionarPedidoCommandHandler(IOptions<RabbitMqConfiguration> option, IPedidoDocumentMapper pedidoDocumentMapper, IPedidoMongoRepository repository)
        {
            _configuration = option.Value;

            _factory = new ConnectionFactory
            {
                UserName = _configuration.UserName,
                Password = _configuration.Password,
                VirtualHost = _configuration.VirtualHost,
                HostName = _configuration.Host
            };

            _pedidoDocumentMapper = pedidoDocumentMapper;
            _repository = repository;
        }
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using (var connection = _factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(
                                queue: _configuration.Queue,
                                durable: true,
                                exclusive: false,
                                autoDelete: false,
                                arguments: null);
                    var consumer = new EventingBasicConsumer(channel);
                    consumer.Received += (sender, eventArgs) =>
                    {
                        var contentArray = eventArgs.Body.ToArray();
                        var contentString = Encoding.UTF8.GetString(contentArray);
                        var message = JsonConvert.DeserializeObject<AdicionarPedidoCommand>(contentString);

                        Handle(message).Wait();
                        channel.BasicAck(eventArgs.DeliveryTag, false);
                    };

                    channel.BasicConsume(_configuration.Queue, false, consumer);
                }

            }
            return Task.CompletedTask;
        }

        public async Task Handle(AdicionarPedidoCommand command)
        {
            var pedidoDocument = await _pedidoDocumentMapper.ConverterAdicionar(command);

            try
            {
                await _repository.CreateAsync(pedidoDocument);
            }
            catch
            {
            }
        }
    }
}
