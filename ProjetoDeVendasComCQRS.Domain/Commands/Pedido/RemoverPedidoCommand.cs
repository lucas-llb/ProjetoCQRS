using System;

namespace ProjetoDeVendasComCQRS.Domain.Commands.Pedido
{
    public class RemoverPedidoCommand : PedidoCommand
    {
        public RemoverPedidoCommand()
        {
        }
        public RemoverPedidoCommand(Guid id)
        {
            Id = id;
        }
    }
}
