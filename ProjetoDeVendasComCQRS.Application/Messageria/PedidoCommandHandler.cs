using ProjetoDeVendasComCQRS.Application.Interfaces;
using ProjetoDeVendasComCQRS.Application.Interfaces.Mappers;
using ProjetoDeVendasComCQRS.Domain.Eventos;
using ProjetoDeVendasComCQRS.Domain.Interfaces.Repository;
using System.Threading.Tasks;

namespace ProjetoDeVendasComCQRS.Application.Messageria
{
    public class PedidoCommandHandler : IPedidoCommandHandler
    {
        private readonly IPedidoDocumentMapper _pedidoDocumentMapper;
        private readonly IPedidoMongoRepository _repository;

        public PedidoCommandHandler(IPedidoDocumentMapper pedidoDocumentMapper, IPedidoMongoRepository repository)
        {
            _pedidoDocumentMapper = pedidoDocumentMapper;
            _repository = repository;
        }
        public async Task Handle(PedidoCriadoEvent message)
        {
            var pedidoDocument = await _pedidoDocumentMapper.ConverterAdicionar(message);

            try
            {
                await _repository.CreateAsync(pedidoDocument);
            }
            catch
            {
            }
        }

        public async Task Handle(PedidoAlteradoEvent message)
        {
            var pedidoDocument = await _pedidoDocumentMapper.ConverterEditar(message);

            try
            {
                await _repository.UpdateAsync(pedidoDocument.IdBanco, pedidoDocument);
            }
            catch
            {
            }
        }

        public async Task Handle(PedidoRemovidoEvent message)
        {
            try
            {
                await _repository.RemoveAsync(message.IdBanco);
            }
            catch
            {
            }
        }
    }
}
