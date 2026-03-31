using MediatR;
using EurekaBack.Application.DTOs;

namespace EurekaBack.Application.Features.Facturas.Commands
{
    public record CreateFacturaCommand(CreateFacturaDto Factura) : IRequest<int>;
}
