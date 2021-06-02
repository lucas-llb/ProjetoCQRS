using System;
using System.Collections.Generic;
using System.Text;

namespace ProjetoDeVendasComCQRS.Domain.Commands.Pedido
{
    public class PedidoCommand : CommandBase
    {
        public Guid Id { get; set; }
        public Guid ProdutoId { get; set; }
        public int Quantidade { get; set; }
        public double ValorTotal { get; set; }
        public DateTime Data { get; set; }
        public Guid ClienteId { get; set; }

        public PedidoCommand()
        {
        }
    }
}
