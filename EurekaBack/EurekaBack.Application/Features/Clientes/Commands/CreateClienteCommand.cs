using MediatR;

namespace EurekaBack.Application.Features.Clientes.Commands
{
    public record CreateClienteCommand(
        string Cc_Nit,
        string Nombre_RazonSocial,
        string Direccion,
        string Telefono,
        bool Estado
    ) : IRequest<int>;
}
