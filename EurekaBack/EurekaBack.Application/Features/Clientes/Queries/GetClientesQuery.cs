using MediatR;
using EurekaBack.Application.DTOs;

namespace EurekaBack.Application.Features.Clientes.Queries
{
    public record GetClientesQuery : IRequest<IEnumerable<ClienteDto>>;
}
