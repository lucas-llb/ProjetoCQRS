using ProjetoDeVendasComCQRS.Application.Interfaces.Mappers;
using ProjetoDeVendasComCQRS.Domain.Document;
using ProjetoDeVendasComCQRS.Domain.Eventos;
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

        public async Task<PedidoDocument> ConverterAdicionar(PedidoCriadoEvent message)
        {
            var produto = await _produtoRepository.GetByIdAsync(message.ProdutoId);
            var valorTotal = produto.Preco * message.Quantidade;
            return new PedidoDocument()
            {
                IdBanco = message.IdBanco,
                ClienteId = message.ClienteId,
                ProdutoId = message.ProdutoId,
                Quantidade = message.Quantidade,
                Data = message.Data,
                ValorTotal = message.ValorTotal,
                UltimaAlteracao = DateTime.Now,
            };
        }

        public async Task<PedidoDocument> ConverterEditar(PedidoAlteradoEvent message)
        {
            var produto = await _produtoRepository.GetByIdAsync(message.ProdutoId);
            var valorTotal = produto.Preco * message.Quantidade;
            var entidade = _pedidoMongoRepository.GetById(message.IdBanco);
            entidade.ClienteId = message.ClienteId;
            entidade.ProdutoId = message.ProdutoId;
            entidade.Quantidade = message.Quantidade;
            entidade.ValorTotal = valorTotal;
            entidade.UltimaAlteracao = DateTime.Now;
            return entidade;
        }
    }
}
