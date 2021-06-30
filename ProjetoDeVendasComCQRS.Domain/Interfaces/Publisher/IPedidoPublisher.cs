using ProjetoDeVendasComCQRS.Domain.Eventos;

namespace ProjetoDeVendasComCQRS.Domain.Interfaces.Publisher
{
    public interface IPedidoPublisher
    {
        void Publisher(PedidoCriadoEvent message);
        void Publisher(PedidoAlteradoEvent message);
        void Publisher(PedidoRemovidoEvent message);
    }
}
