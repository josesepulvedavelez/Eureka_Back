using EurekaBack.Application.DTOs;
using EurekaBack.Domain.Entities;

namespace EurekaBack.Application.Mappers
{
    public static class FacturaMapper
    {
        public static FacturaDto ToDto(Factura factura)
        {
            return new FacturaDto
            {
                FacturaId = factura.FacturaId,
                No = factura.No,
                Fecha = factura.Fecha,
                Total = factura.Total,
                ClienteId = factura.ClienteId,
                Cc_Nit = factura.Cliente?.Cc_Nit,
                Nombre_RazonSocial = factura.Cliente?.Nombre_RazonSocial,
                Direccion = factura.Cliente?.Direccion,
                Telefono = factura.Cliente?.Telefono,
                Estado = factura.Cliente?.Estado,
                lstFacturaDetalleDto = factura.lstFacturaDetalle?.Select(FacturaDetalleMapper.ToDto).ToList() ?? new List<FacturaDetalleDto>()
            };
        }
    }
}
