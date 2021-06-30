using ProjetoDeVendasComCQRS.Application.Interfaces.Mappers;
using ProjetoDeVendasComCQRS.Domain.Commands.Pedido;
using ProjetoDeVendasComCQRS.Domain.Entidades;
using ProjetoDeVendasComCQRS.Domain.Eventos;
using ProjetoDeVendasComCQRS.Domain.Interfaces.Repository;
using System;
using System.Threading.Tasks;

namespace ProjetoDeVendasComCQRS.Application.Mappers
{
    public class PedidoMapper : IPedidoMapper
    {
        private readonly IPedidoRepository _pedidoRepository;
        private readonly IProdutoRepository _produtoRepository;

        public PedidoMapper(IPedidoRepository pedidoRepository, IProdutoRepository produtoRepository)
        {
            _pedidoRepository = pedidoRepository;
            _produtoRepository = produtoRepository;
        }

        public async Task<Pedido> ConverterAdicionar(AdicionarPedidoCommand command)
        {
            var produto = await _produtoRepository.GetByIdAsync(command.ProdutoId);
            var valorTotal = produto.Preco * command.Quantidade;
            return new Pedido()
            {
                ClienteId = command.ClienteId,
                ProdutoId = command.ProdutoId,
                Quantidade = command.Quantidade,
                Data = DateTime.Now,
                ValorTotal = valorTotal
            };
        }

        public async Task<Pedido> ConverterEditar(EditarPedidoCommand command)
        {
            var produto = await _produtoRepository.GetByIdAsync(command.ProdutoId);
            var valorTotal = produto.Preco * command.Quantidade;
            var entidade = await _pedidoRepository.GetByIdAsync(command.Id);
            entidade.ClienteId = command.ClienteId;
            entidade.ProdutoId = command.ProdutoId;
            entidade.Quantidade = command.Quantidade;
            entidade.Data = DateTime.Now;
            entidade.ValorTotal = valorTotal;
            return entidade;
        }

        public PedidoCriadoEvent ConverterPedidoCriadoEvent(Pedido entidade)
        {
            return new PedidoCriadoEvent
            {
                IdBanco = entidade.Id,
                ClienteId = entidade.ClienteId,
                ProdutoId = entidade.ProdutoId,
                Quantidade = entidade.Quantidade,
                ValorTotal = entidade.ValorTotal,
                Data = entidade.Data
            };
        }

        public PedidoAlteradoEvent ConverterPedidoAlteradoEvent(Pedido entidade)
        {
            return new PedidoAlteradoEvent
            {
                IdBanco = entidade.Id,
                ClienteId = entidade.ClienteId,
                ProdutoId = entidade.ProdutoId,
                Quantidade = entidade.Quantidade,
                ValorTotal = entidade.ValorTotal,
                Data = entidade.Data
            };
        }

        public PedidoRemovidoEvent ConverterPedidoRemovidoEvent(Guid id)
        {
            return new PedidoRemovidoEvent
            {
                IdBanco = id
            };
        }
    }
}
