using MediatR;

namespace EurekaBack.Application.Features.Articulos.Commands
{
    public record CreateArticuloCommand(
        string Codigo,
        string Descripcion,
        decimal Costo,
        double Porcentaje,
        decimal PrecioSugerido,
        int Cantidad,
        bool Estado
    ) : IRequest<int>;
}
