using EurekaBack.Application.DTOs;
using EurekaBack.Application.Features.Articulos.Commands;
using EurekaBack.Application.Features.Articulos.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EurekaBack.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticulosController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ArticulosController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("AddArticulo")]
        public async Task<ActionResult> AddArticulo([FromBody] ArticuloDto articulo)
        {
            var command = new CreateArticuloCommand(
                articulo.Codigo,
                articulo.Descripcion,
                articulo.Costo,
                articulo.Porcentaje,
                articulo.PrecioSugerido,
                articulo.Cantidad,
                articulo.Estado
            );

            var id = await _mediator.Send(command);
            return Ok(id);
        }

        [HttpGet("GetArticulos")]
        public async Task<ActionResult> GetArticulos()
        {
            var query = new GetArticulosQuery();
            var articulos = await _mediator.Send(query);
            return Ok(articulos);
        }

        [HttpGet("GetArticulo/{articuloId}")]
        public async Task<ActionResult> GetArticulo(int articuloId)
        {
            var query = new GetArticuloByIdQuery(articuloId);
            var articulo = await _mediator.Send(query);
            return Ok(articulo);
        }

        [HttpPut("UpdateArticulo/{articuloId}")]
        public async Task<ActionResult> UpdateArticulo(int articuloId, [FromBody] ArticuloDto articulo)
        {
            var command = new UpdateArticuloCommand(
                articuloId,
                articulo.Codigo,
                articulo.Descripcion,
                articulo.Costo,
                articulo.Porcentaje,
                articulo.PrecioSugerido,
                articulo.Cantidad,
                articulo.Estado
            );

            var result = await _mediator.Send(command);
            return result ? Ok(articuloId) : NotFound(articuloId);
        }
    }
}
