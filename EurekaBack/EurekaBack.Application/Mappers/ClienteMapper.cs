using EurekaBack.Application.DTOs;
using EurekaBack.Domain.Entities;

namespace EurekaBack.Application.Mappers
{
    public static class ClienteMapper
    {
        public static ClienteDto ToDto(Cliente cliente)
        {
            return new ClienteDto
            {
                ClienteId = cliente.ClienteId,
                Cc_Nit = cliente.Cc_Nit,
                Nombre_RazonSocial = cliente.Nombre_RazonSocial,
                Direccion = cliente.Direccion,
                Telefono = cliente.Telefono,
                Estado = cliente.Estado
            };
        }
    }
}
