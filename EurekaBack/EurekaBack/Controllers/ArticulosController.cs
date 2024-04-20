using EurekaBack.Data;
using EurekaBack.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EurekaBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticulosController : ControllerBase
    {
        EurekaContext _eurekaContext;

        public ArticulosController(EurekaContext eurekaContext)
        { 
            _eurekaContext = eurekaContext;
        }

        [HttpPost("AddArticulo")]
        public async Task<ActionResult> AddArticulo([FromBody] Articulo articulo)
        {
            try
            {
                var art = await _eurekaContext.Articulo.AddAsync(articulo);
                await _eurekaContext.SaveChangesAsync();

                var id = art.Entity.ArticuloId;

                return Ok(id);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno: {ex.Message}");
            }
        }

        [HttpGet("GetArticulos")]
        public async Task<ActionResult> GetArticulos()
        {
            try
            {
                var articulos = await _eurekaContext.Articulo.ToListAsync();
                return Ok(articulos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno: {ex.Message}");
            }
        }

        [HttpGet("GetArticulo/{articuloId}")]
        public async Task<ActionResult> GetArticulo(int articuloId)
        {
            try
            {
                var clientes = await _eurekaContext.Articulo.FindAsync(articuloId);
                return Ok(clientes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno: {ex.Message}");
            }
        }

        [HttpPut("UpdateArticulo/{articuloId}")]
        public async Task<ActionResult> UpdateArticulo(int articuloId, [FromBody] Articulo articulo)
        {
            try
            {
                var existingArticulo = await _eurekaContext.Articulo.FindAsync(articuloId);

                if (existingArticulo == null)
                {
                    return NotFound(articuloId);
                }

                existingArticulo.Codigo = articulo.Codigo;
                existingArticulo.Descripcion = articulo.Descripcion;                
                existingArticulo.Costo = articulo.Costo;               
                existingArticulo.Porcentaje = articulo.Porcentaje;
                existingArticulo.PrecioSugerido = articulo.PrecioSugerido;
				existingArticulo.Cantidad = articulo.Cantidad;
				existingArticulo.Estado = articulo.Estado;

                await _eurekaContext.SaveChangesAsync();

                return Ok(articuloId);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno al actualizar el cliente: {ex.Message}");
            }
        }

    }
}
