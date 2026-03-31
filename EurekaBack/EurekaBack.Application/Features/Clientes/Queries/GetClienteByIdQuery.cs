using MediatR;
using EurekaBack.Application.DTOs;

namespace EurekaBack.Application.Features.Clientes.Queries
{
    public record GetClienteByIdQuery(int ClienteId) : IRequest<ClienteDto?>;
}
