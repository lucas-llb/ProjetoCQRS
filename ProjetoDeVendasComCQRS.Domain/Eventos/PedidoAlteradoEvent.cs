using System;

namespace ProjetoDeVendasComCQRS.Domain.Eventos
{
    public class PedidoAlteradoEvent
    {
        public Guid IdBanco { get; set; }
        public Guid ProdutoId { get; set; }
        public Guid ClienteId { get; set; }
        public int Quantidade { get; set; }
        public double ValorTotal { get; set; }
        public DateTime Data { get; set; }

        public PedidoAlteradoEvent()
        {
        }
    }
}
