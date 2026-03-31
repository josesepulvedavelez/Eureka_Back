using MediatR;
using EurekaBack.Application.DTOs;

namespace EurekaBack.Application.Features.Facturas.Queries
{
    public record GetFacturasQuery : IRequest<IEnumerable<FacturaDto>>;
}
