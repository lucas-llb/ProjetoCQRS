﻿using ProjetoDeVendasComCQRS.Domain.Commands.Pedido;
using ProjetoDeVendasComCQRS.Domain.Entidades;
using ProjetoDeVendasComCQRS.Domain.Eventos;
using System;
using System.Threading.Tasks;

namespace ProjetoDeVendasComCQRS.Application.Interfaces.Mappers
{
    public interface IPedidoMapper
    {
        Task<Pedido> ConverterAdicionar(AdicionarPedidoCommand command);
        Task<Pedido> ConverterEditar(EditarPedidoCommand command);
        PedidoCriadoEvent ConverterPedidoCriadoEvent(Pedido entidade);
        PedidoAlteradoEvent ConverterPedidoAlteradoEvent(Pedido entidade);
        PedidoRemovidoEvent ConverterPedidoRemovidoEvent(Guid id);
    }
}
