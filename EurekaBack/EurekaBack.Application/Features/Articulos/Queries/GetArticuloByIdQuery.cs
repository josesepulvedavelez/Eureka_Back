using MediatR;
using EurekaBack.Application.DTOs;

namespace EurekaBack.Application.Features.Articulos.Queries
{
    public record GetArticuloByIdQuery(int ArticuloId) : IRequest<ArticuloDto?>;
}
