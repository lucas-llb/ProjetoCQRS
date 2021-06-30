using ProjetoDeVendasComCQRS.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjetoDeVendasComCQRS.Domain.Interfaces.Repository
{
    public interface IPedidoRepository
    {
        Task<Pedido> GetByIdAsync(Guid id);
        Task<IEnumerable<Pedido>> GetAllAsync();
        Task CreateAsync(Pedido entity);
        Task UpdateAsync(Pedido entity);
        Task DeleteAsync(Guid id);
        Task<Pedido> CreateAsyncWithReturn(Pedido entity);
        Task<Pedido> UpdateAsyncWithReturn(Pedido entity);
        bool VerificarSePedidoExiste(Guid id);
    }
}
