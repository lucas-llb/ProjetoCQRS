using EasyNetQ;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ProjetoDeVendasComCQRS.Application.Interfaces;
using ProjetoDeVendasComCQRS.Domain.Eventos;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ProjetoDeVendasComCQRS.Infra.Menssageria
{
    public class MensageriaHostedService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        public MensageriaHostedService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {

            return Task.Run(() =>
            {
                var bus = RabbitHutch.CreateBus("host=localhost;virtualHost=localhost;prefetchcount=1");

                bus.PubSub.SubscribeAsync<PedidoCriadoEvent>("", async msg =>
                {
                    using (var container = _serviceProvider.CreateScope())
                    {
                        var adicionarPedidoCommandHandler = container.ServiceProvider.GetRequiredService<IPedidoCommandHandler>();
                        await adicionarPedidoCommandHandler.Handle(msg);
                    }
                }
                );

                bus.PubSub.SubscribeAsync<PedidoAlteradoEvent>("", async msg =>
                {
                    using (var container = _serviceProvider.CreateScope())
                    {
                        var adicionarPedidoCommandHandler = container.ServiceProvider.GetRequiredService<IPedidoCommandHandler>();
                        await adicionarPedidoCommandHandler.Handle(msg);
                    }
                }
                );

                bus.PubSub.SubscribeAsync<PedidoRemovidoEvent>("", async msg =>
                {
                    using (var container = _serviceProvider.CreateScope())
                    {
                        var adicionarPedidoCommandHandler = container.ServiceProvider.GetRequiredService<IPedidoCommandHandler>();
                        await adicionarPedidoCommandHandler.Handle(msg);
                    }
                }
                );

            }, stoppingToken);
        }

    }
}
