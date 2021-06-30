using EasyNetQ;
using ProjetoDeVendasComCQRS.Domain.Entidades;
using ProjetoDeVendasComCQRS.Domain.Eventos;
using ProjetoDeVendasComCQRS.Domain.Interfaces.Publisher;

namespace ProjetoDeVendasComCQRS.Infra.Publisher
{
    public class PedidoPublisher : IPedidoPublisher
    {
        public void Publisher(PedidoCriadoEvent message)
        {
            using (var bus = RabbitHutch.CreateBus("host=localhost;virtualHost=localhost"))
            {
                bus.PubSub.Publish(message);
            }
        }

        public void Publisher(PedidoAlteradoEvent message)
        {
            using (var bus = RabbitHutch.CreateBus("host=localhost;virtualHost=localhost"))
            {
                bus.PubSub.Publish(message);
            }
        }

        public void Publisher(PedidoRemovidoEvent message)
        {
            using (var bus = RabbitHutch.CreateBus("host=localhost;virtualHost=localhost"))
            {
                bus.PubSub.Publish(message);
            }
        }
    }
}
