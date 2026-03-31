using MediatR;
using EurekaBack.Application.DTOs;

namespace EurekaBack.Application.Features.Facturas.Queries
{
    public record GetFacturaDetalleQuery(int FacturaId) : IRequest<IEnumerable<FacturaDetalleDto>>;
}
