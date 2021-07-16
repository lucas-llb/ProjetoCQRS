using ProjetoDeVendasComCQRS.Domain.Commands.Pedido;
using ProjetoDeVendasComCQRS.Domain.Document;
using ProjetoDeVendasComCQRS.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjetoDeVendasComCQRS.Application.Interfaces.Services
{
    public interface IPedidoService
    {
        Task<ResponseToUser> CreateAsync(AdicionarPedidoCommand command);
        Task<ResponseToUser> UpdateAsync(EditarPedidoCommand command);
        Task<ResponseToUser> RemoveAsync(RemoverPedidoCommand command);
        IEnumerable<PedidoDocument> GetAllAsync();
        IEnumerable<PedidoDocument> ListarPorCliente(Guid clienteId);
    }
}
