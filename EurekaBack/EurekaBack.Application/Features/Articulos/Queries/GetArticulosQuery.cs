using MediatR;
using EurekaBack.Application.DTOs;

namespace EurekaBack.Application.Features.Articulos.Queries
{
    public record GetArticulosQuery : IRequest<IEnumerable<ArticuloDto>>;
}
