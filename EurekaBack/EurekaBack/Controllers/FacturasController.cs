using EurekaBack.Data;
using EurekaBack.Dto;
using EurekaBack.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EurekaBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacturasController : ControllerBase
    {
        EurekaContext _eurekaContext;
        public FacturasController(EurekaContext eurekaContext) 
        { 
            _eurekaContext = eurekaContext;
        }

        [HttpGet("GetFacturas")]
        public async Task<ActionResult> GetFacturas() 
        {
            try
            {
                var facturas = await (from cliente in _eurekaContext.Cliente 
                                        join factura in _eurekaContext.Factura
                                        on cliente.ClienteId equals factura.ClienteId
                                      
                                      select new FacturaDto
                                      { 
                                            Cc_Nit = cliente.Cc_Nit,
                                            Nombre_RazonSocial = cliente.Nombre_RazonSocial,
                                            Direccion = cliente.Direccion,
                                            Telefono = cliente.Telefono,
                                            Estado = cliente.Estado,
                                            ClienteId = cliente.ClienteId,
                                            No = factura.No,
                                            Fecha = factura.Fecha,
                                            Total = factura.Total,
                                            FacturaId = factura.FacturaId
                                      }).ToListAsync();                    

                return Ok(facturas);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno: { ex.Message }");
            }
        }

        [HttpGet("GetFacturas/{fechaInicial}/{fechaFinal}")]
        public async Task<ActionResult> GetFacturas(DateTime fechaInicial, DateTime fechaFinal)
        {
            try
            {
                var facturas = await (from cliente in _eurekaContext.Cliente
                                      join factura in _eurekaContext.Factura
                                      on cliente.ClienteId equals factura.ClienteId
                                      where factura.Fecha >= fechaInicial & factura.Fecha <= fechaFinal
                                      select new FacturaDto
                                      {
                                          Cc_Nit = cliente.Cc_Nit,
                                          Nombre_RazonSocial = cliente.Nombre_RazonSocial,
                                          Direccion = cliente.Direccion,
                                          Telefono = cliente.Telefono,
                                          Estado = cliente.Estado,
                                          ClienteId = cliente.ClienteId,
                                          No = factura.No,
                                          Fecha = factura.Fecha,
                                          Total = factura.Total,
                                          FacturaId = factura.FacturaId
                                      }).ToListAsync();

                return Ok(facturas);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno: {ex.Message}");
            }
        }

        [HttpGet("GetFactura/{facturaId}")]
		public async Task<ActionResult> GetFactura(int facturaId)
		{
			try
			{
				var facturaUnica = await (from cliente in _eurekaContext.Cliente
									 join factura in _eurekaContext.Factura
									 on cliente.ClienteId equals factura.ClienteId
									 where factura.FacturaId == facturaId
									 select new FacturaDto
									 {
                                         Cc_Nit = cliente.Cc_Nit,
                                         Nombre_RazonSocial = cliente.Nombre_RazonSocial,
                                         Direccion = cliente.Direccion,
										 Telefono = cliente.Telefono,
										 Estado = cliente.Estado,
										 ClienteId = cliente.ClienteId,
										 No = factura.No,
										 Fecha = factura.Fecha,
										 Total = factura.Total,
										 FacturaId = factura.FacturaId
									 }).FirstOrDefaultAsync();

				return Ok(facturaUnica);
			}
			catch (Exception ex)
			{
				return StatusCode(500, $"Error interno: {ex.Message}");
			}
		}

        [HttpGet("GetFacturaDetalle/{facturaId}")]
        public async Task<ActionResult> GetFacturaDetalle(int facturaId)
        {
            try
            {
                var facturaDetalles = await (from articulo in _eurekaContext.Articulo
                                                join facturaDetalle in _eurekaContext.FacturaDetalle
                                                on articulo.ArticuloId equals facturaDetalle.ArticuloId
                                                where facturaDetalle.FacturaId == facturaId
                                                select new FacturaDetalleDto
                                                { 
                                                    Codigo = articulo.Codigo,
                                                    Descripcion = articulo.Descripcion,
                                                    ArticuloId = articulo.ArticuloId,
                                                    Precio = facturaDetalle.Precio,
                                                    Cantidad = facturaDetalle.Cantidad,
                                                    SubTotal = facturaDetalle.SubTotal,
                                                    FacturaId = facturaDetalle.FacturaId,
                                                    FacturaDetalleId = facturaDetalle.FacturaDetalleId
                                                }).ToListAsync();
                return Ok(facturaDetalles);

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno: {ex.Message}");
            }
        }

        [HttpPost("AddFactura")]
        public async Task<ActionResult> AddFactura([FromBody] FacturaDto facturaDto)
        {
            try
            {
                using (var transaccion = _eurekaContext.Database.BeginTransactionAsync())
                {
                    Factura _factura = new()
                    {
                        No = facturaDto.No,
                        Fecha = facturaDto.Fecha,                        
                        ClienteId = facturaDto.ClienteId
                    };

                    await _eurekaContext.Factura.AddAsync(_factura);
                    await _eurekaContext.SaveChangesAsync();


                    var idFactura = _factura.FacturaId;

                    foreach (var item in facturaDto.lstFacturaDetalleDto)
                    {
                        FacturaDetalle _facturaDetalle = new()
                        {
                            Cantidad = item.Cantidad,
                            Precio = item.Precio,
                            SubTotal = item.Cantidad * item.Precio,
                            FacturaId = idFactura,
                            ArticuloId = item.ArticuloId
                        };

                        var articulo = await _eurekaContext.Articulo.FindAsync(item.ArticuloId);

                        if (articulo != null)
                        {
                            articulo.Cantidad -= item.Cantidad;
                        }

                        _factura.Total += _facturaDetalle.SubTotal;

                        await _eurekaContext.FacturaDetalle.AddAsync(_facturaDetalle);
                    }

                    await _eurekaContext.SaveChangesAsync();

                    await _eurekaContext.Database.CommitTransactionAsync();

                    return Ok(facturaDto);
                }
            }
            catch (Exception ex)
            {
                await _eurekaContext.Database.RollbackTransactionAsync();
                return StatusCode(500, $"Error interno: {ex.Message}");
            }
        }

	}
}
