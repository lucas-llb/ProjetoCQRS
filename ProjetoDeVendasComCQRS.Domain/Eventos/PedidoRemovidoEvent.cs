using System;

namespace ProjetoDeVendasComCQRS.Domain.Eventos
{
    public class PedidoRemovidoEvent
    {
        public Guid IdBanco { get; set; }

        public PedidoRemovidoEvent()
        {
        }
    }
}
