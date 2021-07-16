using Microsoft.AspNetCore.Mvc;
using ProjetoDeVendasComCQRS.Application.Interfaces.Services;
using ProjetoDeVendasComCQRS.Domain.Commands.Pedido;
using System;
using System.Threading.Tasks;

namespace ProjetoDeVendasComCQRS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidoController : ControllerBase
    {
        private readonly IPedidoService _pedidoService;

        public PedidoController(IPedidoService pedidoService)
        {
            _pedidoService = pedidoService;
        }

        [HttpPost]
        [Route("cadastrar")]
        public async Task<IActionResult> Post(AdicionarPedidoCommand command)
        {
            var result = await _pedidoService.CreateAsync(command);
            if (result.Success)
            {
                return Ok("Pedido cadastrado com sucesso!");
            }
            else
            {
                return BadRequest(result.Messages);
            }
        }

        [HttpPut]
        [Route("editar")]
        public async Task<IActionResult> Put(EditarPedidoCommand command)
        {
            var result = await _pedidoService.UpdateAsync(command);
            if (result.Success)
            {
                return Ok("Pedido alterado com sucesso!");
            }
            else
            {
                return BadRequest(result.Messages);
            }
        }

        [HttpDelete]
        [Route("remover")]
        public async Task<IActionResult> Delete(RemoverPedidoCommand command)
        {
            var result = await _pedidoService.RemoveAsync(command);
            if (result.Success)
            {
                return Ok("Pedido deletado com sucesso!");
            }
            else
            {
                return BadRequest(result.Messages);
            }

        }

        [HttpGet]
        [Route("listar")]
        public IActionResult Get()
        {
            try
            {
                return Ok(_pedidoService.GetAllAsync());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("listarporcliente")]
        public IActionResult ListarPorCliente([FromQuery] Guid clienteId)
        {
            try
            {
                return Ok(_pedidoService.ListarPorCliente(clienteId));
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
