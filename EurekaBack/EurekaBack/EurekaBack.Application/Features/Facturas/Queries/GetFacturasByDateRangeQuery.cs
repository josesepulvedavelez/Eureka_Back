using MediatR;
using EurekaBack.Application.DTOs;

namespace EurekaBack.Application.Features.Facturas.Queries
{
    public record GetFacturasByDateRangeQuery(DateTime FechaInicial, DateTime FechaFinal) : IRequest<IEnumerable<FacturaDto>>;
}
