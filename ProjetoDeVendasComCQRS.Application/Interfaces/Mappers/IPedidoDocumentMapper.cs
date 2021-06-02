using ProjetoDeVendasComCQRS.Domain.Commands.Pedido;
using ProjetoDeVendasComCQRS.Domain.Document;
using System.Threading.Tasks;

namespace ProjetoDeVendasComCQRS.Application.Interfaces.Mappers
{
    public interface IPedidoDocumentMapper
    {
        Task<PedidoDocument> ConverterAdicionar(AdicionarPedidoCommand command);
        Task<PedidoDocument> ConverterEditar(EditarPedidoCommand command);
    }
}
