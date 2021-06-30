using ProjetoDeVendasComCQRS.Domain.Eventos;
using System.Threading.Tasks;

namespace ProjetoDeVendasComCQRS.Application.Interfaces
{
    public interface IPedidoCommandHandler
    {
        Task Handle(PedidoCriadoEvent message);
        Task Handle(PedidoAlteradoEvent message);
        Task Handle(PedidoRemovidoEvent message);
    }
}
