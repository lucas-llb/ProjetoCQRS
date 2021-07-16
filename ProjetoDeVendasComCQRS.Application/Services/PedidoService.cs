using FluentValidation;
using ProjetoDeVendasComCQRS.Application.Interfaces.Mappers;
using ProjetoDeVendasComCQRS.Application.Interfaces.Services;
using ProjetoDeVendasComCQRS.Domain.Commands.Pedido;
using ProjetoDeVendasComCQRS.Domain.Document;
using ProjetoDeVendasComCQRS.Domain.Interfaces.Publisher;
using ProjetoDeVendasComCQRS.Domain.Interfaces.Repository;
using ProjetoDeVendasComCQRS.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoDeVendasComCQRS.Application.Services
{
    public class PedidoService : BaseServices, IPedidoService
    {
        private readonly IPedidoRepository _pedidoRepository;
        private readonly IValidator<AdicionarPedidoCommand> _adicionarValidator;
        private readonly IValidator<EditarPedidoCommand> _editarValidator;
        private readonly IValidator<RemoverPedidoCommand> _removerValidator;
        private readonly IPedidoMapper _pedidoMapper;
        private readonly IPedidoMongoRepository _pedidoMongoRepository;
        private readonly IPedidoPublisher _publisher;

        public PedidoService(
            IPedidoRepository pedidoRepository,
            IValidator<AdicionarPedidoCommand> adicionarValidator,
            IValidator<EditarPedidoCommand> editarValidator,
            IValidator<RemoverPedidoCommand> removerValidator,
            IPedidoMapper pedidoMapper,
            IPedidoMongoRepository pedidoMongoRepository,
            IPedidoPublisher publisher)
        {
            _pedidoRepository = pedidoRepository;
            _adicionarValidator = adicionarValidator;
            _editarValidator = editarValidator;
            _removerValidator = removerValidator;
            _pedidoMapper = pedidoMapper;
            _pedidoMongoRepository = pedidoMongoRepository;
            _publisher = publisher;
        }

        public async Task<ResponseToUser> CreateAsync(AdicionarPedidoCommand command)
        {
            var result = _adicionarValidator.Validate(command);
            if (!result.IsValid)
            {
                return ErrorResult(result.Errors.Select(q => q.ErrorMessage));
            }
            var entidade = await _pedidoMapper.ConverterAdicionar(command);
            var entidadePersistida = await _pedidoRepository.CreateAsyncWithReturn(entidade);

            _publisher.Publisher(_pedidoMapper.ConverterPedidoCriadoEvent(entidadePersistida));

            return SuccessResult();
        }

        public async Task<ResponseToUser> UpdateAsync(EditarPedidoCommand command)
        {
            var result = _editarValidator.Validate(command);
            if (!result.IsValid)
            {
                return ErrorResult(result.Errors.Select(q => q.ErrorMessage));
            }
            var entidade = await _pedidoMapper.ConverterEditar(command);
            var entidadePersistida = await _pedidoRepository.UpdateAsyncWithReturn(entidade);

            _publisher.Publisher(_pedidoMapper.ConverterPedidoAlteradoEvent(entidadePersistida));

            return SuccessResult();
        }

        public async Task<ResponseToUser> RemoveAsync(RemoverPedidoCommand command)
        {
            var result = _removerValidator.Validate(command);
            if (!result.IsValid)
            {
                return ErrorResult(result.Errors.Select(q => q.ErrorMessage));
            }
            await _pedidoRepository.DeleteAsync(command.Id);

            _publisher.Publisher(_pedidoMapper.ConverterPedidoRemovidoEvent(command.Id));

            return SuccessResult();
        }

        public IEnumerable<PedidoDocument> GetAllAsync()
        {
            return _pedidoMongoRepository.Get();
        }

        public IEnumerable<PedidoDocument> ListarPorCliente(Guid clienteId)
        {
            return _pedidoMongoRepository.ListarPorCliente(clienteId);
        }

    }

}
