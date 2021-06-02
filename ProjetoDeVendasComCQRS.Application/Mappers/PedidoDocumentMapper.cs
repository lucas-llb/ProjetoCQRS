using ProjetoDeVendasComCQRS.Application.Interfaces.Mappers;
using ProjetoDeVendasComCQRS.Domain.Commands.Pedido;
using ProjetoDeVendasComCQRS.Domain.Document;
using ProjetoDeVendasComCQRS.Domain.Interfaces.Repository;
using System;
using System.Threading.Tasks;

namespace ProjetoDeVendasComCQRS.Application.Mappers
{
    public class PedidoDocumentMapper : IPedidoDocumentMapper
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly IPedidoMongoRepository _pedidoMongoRepository;

        public PedidoDocumentMapper(IProdutoRepository produtoRepository, IPedidoMongoRepository pedidoMongoRepository)
        {
            _produtoRepository = produtoRepository;
            _pedidoMongoRepository = pedidoMongoRepository;
        }

        public async Task<PedidoDocument> ConverterAdicionar(AdicionarPedidoCommand command)
        {
            var produto = await _produtoRepository.GetByIdAsync(command.ProdutoId);
            var valorTotal = produto.Preco * command.Quantidade;
            return new PedidoDocument()
            {
                ClienteId = command.ClienteId,
                ProdutoId = command.ProdutoId,
                Quantidade = command.Quantidade,
                Data = DateTime.Now,
                ValorTotal = valorTotal
            };
        }

        public async Task<PedidoDocument> ConverterEditar(EditarPedidoCommand command)
        {
            var produto = await _produtoRepository.GetByIdAsync(command.ProdutoId);
            var valorTotal = produto.Preco * command.Quantidade;
            var entidade = _pedidoMongoRepository.GetById(command.Id);
            entidade.ClienteId = command.ClienteId;
            entidade.ProdutoId = command.ProdutoId;
            entidade.Quantidade = command.Quantidade;
            entidade.Data = DateTime.Now;
            entidade.ValorTotal = valorTotal;
            return entidade;
        }
    }
}
