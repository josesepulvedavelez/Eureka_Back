using EurekaBack.Data;
using EurekaBack.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EurekaBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        EurekaContext _eurekaContext;

        public ClientesController(EurekaContext eurekaContext) 
        {
            _eurekaContext = eurekaContext;
        }

        [HttpPost("AddCliente")]
        public async Task<ActionResult> AddCliente([FromBody] Cliente cliente)
        {
            try
            {
                var cli = await _eurekaContext.Cliente.AddAsync(cliente);
                await _eurekaContext.SaveChangesAsync();

                var id = cli.Entity.ClienteId;

                return Ok(id);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno: {ex.Message}");
            }
        }

        [HttpGet("GetClientes")]
        public async Task<ActionResult> GetClientes()
        {
            try
            {
                var clientes = await _eurekaContext.Cliente.ToListAsync();
                return Ok(clientes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno: {ex.Message}");
            }            
        }

        [HttpGet("GetCliente/{clienteId}")]
        public async Task<ActionResult> GetCliente(int clienteId)
        {
            try
            {
                var clientes = await _eurekaContext.Cliente.FindAsync(clienteId);
                return Ok(clientes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno: {ex.Message}");
            }
        }

        [HttpPut("UpdateCliente/{clienteId}")]
        public async Task<ActionResult> UpdateCliente(int clienteId, [FromBody] Cliente cliente)
        {
            try
            {
                var existingCliente = await _eurekaContext.Cliente.FindAsync(clienteId);

                if (existingCliente == null)
                {
                    return NotFound(clienteId);
                }

                existingCliente.Cc_Nit = cliente.Direccion;
                existingCliente.Nombre_RazonSocial = cliente.Nombre_RazonSocial;
                existingCliente.Direccion = cliente.Direccion;
                existingCliente.Telefono = cliente.Telefono;
                existingCliente.Estado = cliente.Estado;
                                
                await _eurekaContext.SaveChangesAsync();

                return Ok(clienteId);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno al actualizar el cliente: {ex.Message}");
            }
        }


    }
}
