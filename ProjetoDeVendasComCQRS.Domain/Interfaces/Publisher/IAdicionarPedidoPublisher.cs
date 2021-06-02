using ProjetoDeVendasComCQRS.Domain.Commands.Pedido;

namespace ProjetoDeVendasComCQRS.Domain.Interfaces.Publisher
{
    public interface IAdicionarPedidoPublisher
    {
        void Publisher(AdicionarPedidoCommand command);
    }
}
