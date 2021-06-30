using ProjetoDeVendasComCQRS.Domain.Document;
using ProjetoDeVendasComCQRS.Domain.Eventos;
using System.Threading.Tasks;

namespace ProjetoDeVendasComCQRS.Application.Interfaces.Mappers
{
    public interface IPedidoDocumentMapper
    {
        Task<PedidoDocument> ConverterAdicionar(PedidoCriadoEvent message);
        Task<PedidoDocument> ConverterEditar(PedidoAlteradoEvent message);
    }
}
