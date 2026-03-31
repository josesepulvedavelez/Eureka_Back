using MediatR;

namespace EurekaBack.Application.Features.Articulos.Commands
{
    public record UpdateArticuloCommand(
        int ArticuloId,
        string Codigo,
        string Descripcion,
        decimal Costo,
        double Porcentaje,
        decimal PrecioSugerido,
        int Cantidad,
        bool Estado
    ) : IRequest<bool>;
}
