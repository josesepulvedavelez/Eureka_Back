using EurekaBack.Application.DTOs;
using EurekaBack.Domain.Entities;

namespace EurekaBack.Application.Mappers
{
    public static class FacturaDetalleMapper
    {
        public static FacturaDetalleDto ToDto(FacturaDetalle detalle)
        {
            return new FacturaDetalleDto
            {
                FacturaDetalleId = detalle.FacturaDetalleId,
                FacturaId = detalle.FacturaId,
                ArticuloId = detalle.ArticuloId,
                Codigo = detalle.Articulo?.Codigo ?? string.Empty,
                Descripcion = detalle.Articulo?.Descripcion ?? string.Empty,
                Precio = detalle.Precio,
                Cantidad = detalle.Cantidad,
                SubTotal = detalle.SubTotal
            };
        }
    }
}
