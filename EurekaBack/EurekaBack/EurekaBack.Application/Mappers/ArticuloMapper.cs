using EurekaBack.Application.DTOs;
using EurekaBack.Domain.Entities;

namespace EurekaBack.Application.Mappers
{
    public static class ArticuloMapper
    {
        public static ArticuloDto ToDto(Articulo articulo)
        {
            return new ArticuloDto
            {
                ArticuloId = articulo.ArticuloId,
                Codigo = articulo.Codigo,
                Descripcion = articulo.Descripcion,
                Costo = articulo.Costo,
                Porcentaje = articulo.Porcentaje,
                PrecioSugerido = articulo.PrecioSugerido,
                Cantidad = articulo.Cantidad,
                Estado = articulo.Estado
            };
        }
    }
}
