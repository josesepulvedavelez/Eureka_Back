using MediatR;
using EurekaBack.Application.DTOs;

namespace EurekaBack.Application.Features.Facturas.Queries
{
    public record GetFacturaByIdQuery(int FacturaId) : IRequest<FacturaDto?>;
}
