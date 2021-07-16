using ProjetoDeVendasComCQRS.Domain.Document;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjetoDeVendasComCQRS.Domain.Interfaces.Repository
{
    public interface IPedidoMongoRepository
    {
        List<PedidoDocument> Get();
        PedidoDocument GetById(Guid id);
        Task<PedidoDocument> CreateAsync(PedidoDocument pedido);
        Task UpdateAsync(Guid id, PedidoDocument pedido);
        Task RemoveAsync(Guid id);
        IEnumerable<PedidoDocument> ListarPorCliente(Guid clienteId);
    }
}
