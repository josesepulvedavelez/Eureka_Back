using MediatR;

namespace EurekaBack.Application.Features.Clientes.Commands
{
    public record UpdateClienteCommand(
        int ClienteId,
        string Cc_Nit,
        string Nombre_RazonSocial,
        string Direccion,
        string Telefono,
        bool Estado
    ) : IRequest<bool>;
}
