using EurekaBack.Application.DTOs;
using EurekaBack.Application.Features.Facturas.Commands;
using EurekaBack.Application.Features.Facturas.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EurekaBack.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacturasController : ControllerBase
    {
        private readonly IMediator _mediator;

        public FacturasController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("GetFacturas")]
        public async Task<ActionResult> GetFacturas()
        {
            var query = new GetFacturasQuery();
            var facturas = await _mediator.Send(query);
            return Ok(facturas);
        }

        [HttpGet("GetFacturas/{fechaInicial}/{fechaFinal}")]
        public async Task<ActionResult> GetFacturas(DateTime fechaInicial, DateTime fechaFinal)
        {
            var query = new GetFacturasByDateRangeQuery(fechaInicial, fechaFinal);
            var facturas = await _mediator.Send(query);
            return Ok(facturas);
        }

        [HttpGet("GetFactura/{facturaId}")]
        public async Task<ActionResult> GetFactura(int facturaId)
        {
            var query = new GetFacturaByIdQuery(facturaId);
            var factura = await _mediator.Send(query);
            return Ok(factura);
        }

        [HttpGet("GetFacturaDetalle/{facturaId}")]
        public async Task<ActionResult> GetFacturaDetalle(int facturaId)
        {
            var query = new GetFacturaDetalleQuery(facturaId);
            var detalles = await _mediator.Send(query);
            return Ok(detalles);
        }

        [HttpPost("AddFactura")]
        public async Task<ActionResult> AddFactura([FromBody] CreateFacturaDto facturaDto)
        {
            var command = new CreateFacturaCommand(facturaDto);
            var id = await _mediator.Send(command);
            return Ok(new { FacturaId = id });
        }
    }
}
