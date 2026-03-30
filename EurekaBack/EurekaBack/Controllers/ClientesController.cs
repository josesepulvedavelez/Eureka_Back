using EurekaBack.Application.DTOs;
using EurekaBack.Application.Features.Clientes.Commands;
using EurekaBack.Application.Features.Clientes.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EurekaBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ClientesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("AddCliente")]
        public async Task<ActionResult> AddCliente([FromBody] ClienteDto cliente)
        {
            var command = new CreateClienteCommand(
                cliente.Cc_Nit,
                cliente.Nombre_RazonSocial,
                cliente.Direccion,
                cliente.Telefono,
                cliente.Estado
            );

            var id = await _mediator.Send(command);
            return Ok(id);
        }

        [HttpGet("GetClientes")]
        public async Task<ActionResult> GetClientes()
        {
            var query = new GetClientesQuery();
            var clientes = await _mediator.Send(query);
            return Ok(clientes);
        }

        [HttpGet("GetCliente/{clienteId}")]
        public async Task<ActionResult> GetCliente(int clienteId)
        {
            var query = new GetClienteByIdQuery(clienteId);
            var cliente = await _mediator.Send(query);
            return Ok(cliente);
        }

        [HttpPut("UpdateCliente/{clienteId}")]
        public async Task<ActionResult> UpdateCliente(int clienteId, [FromBody] ClienteDto cliente)
        {
            var command = new UpdateClienteCommand(
                clienteId,
                cliente.Cc_Nit,
                cliente.Nombre_RazonSocial,
                cliente.Direccion,
                cliente.Telefono,
                cliente.Estado
            );

            var result = await _mediator.Send(command);
            return result ? Ok(clienteId) : NotFound(clienteId);
        }
    }
}
