﻿using Microsoft.Extensions.Hosting;
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
    public class EditarPedidoCommandHandler : BackgroundService
    {
        private readonly RabbitMqConfiguration _configuration;
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly IPedidoDocumentMapper _pedidoDocumentMapper;
        private readonly IPedidoMongoRepository _repository;
        //public EditarPedidoCommandHandler(IOptions<RabbitMqConfiguration> option, IPedidoDocumentMapper pedidoDocumentMapper, IPedidoMongoRepository repository)
        //{
        //    _configuration = option.Value;

        //    var factory = new ConnectionFactory
        //    {
        //        UserName = "guest",
        //        Password = "guest",
        //        VirtualHost = "localhost",
        //        HostName = _configuration.Host
        //    };

        //    _connection = factory.CreateConnection();
        //    _channel = _connection.CreateModel();
        //    _channel.QueueDeclare(
        //        queue: _configuration.Queue,
        //        durable: false,
        //        exclusive: false,
        //        autoDelete: false,
        //        arguments: null);

        //    _pedidoDocumentMapper = pedidoDocumentMapper;
        //    _repository = repository;
        //}
        //protected override Task ExecuteAsync(CancellationToken stoppingToken)
        //{
        //    var consumer = new EventingBasicConsumer(_channel);

        //    consumer.Received += (sender, eventArgs) =>
        //    {
        //        var contentArray = eventArgs.Body.ToArray();
        //        var contentString = Encoding.UTF8.GetString(contentArray);
        //        var message = JsonConvert.DeserializeObject<EditarPedidoCommand>(contentString);

        //        Handle(message).Wait();
        //        _channel.BasicAck(eventArgs.DeliveryTag, false);
        //    };

        //    _channel.BasicConsume(_configuration.Queue, false, consumer);

        //    return Task.CompletedTask;
        //}

        public async Task Handle(EditarPedidoCommand command)
        {
            var pedidoDocument = await _pedidoDocumentMapper.ConverterEditar(command);

            try
            {
                await _repository.UpdateAsync(pedidoDocument.IdBanco, pedidoDocument);
            }
            catch
            {
            }
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            throw new System.NotImplementedException();
        }
    }
}