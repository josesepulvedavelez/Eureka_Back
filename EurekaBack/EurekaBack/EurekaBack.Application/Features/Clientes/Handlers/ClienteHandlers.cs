using EurekaBack.Application.DTOs;
using EurekaBack.Application.Features.Clientes.Commands;
using EurekaBack.Application.Features.Clientes.Queries;
using EurekaBack.Application.Mappers;
using EurekaBack.Domain.Entities;
using EurekaBack.Domain.Interfaces;
using MediatR;

namespace EurekaBack.Application.Features.Clientes.Handlers
{
    public class GetClientesHandler : IRequestHandler<GetClientesQuery, IEnumerable<ClienteDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetClientesHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<ClienteDto>> Handle(GetClientesQuery request, CancellationToken cancellationToken)
        {
            var clientes = await _unitOfWork.Clientes.GetAllAsync();
            return clientes.Select(ClienteMapper.ToDto);
        }
    }

    public class GetClienteByIdHandler : IRequestHandler<GetClienteByIdQuery, ClienteDto?>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetClienteByIdHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ClienteDto?> Handle(GetClienteByIdQuery request, CancellationToken cancellationToken)
        {
            var cliente = await _unitOfWork.Clientes.GetByIdAsync(request.ClienteId);
            return cliente != null ? ClienteMapper.ToDto(cliente) : null;
        }
    }

    public class CreateClienteHandler : IRequestHandler<CreateClienteCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateClienteHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(CreateClienteCommand request, CancellationToken cancellationToken)
        {
            var cliente = new Cliente
            {
                Cc_Nit = request.Cc_Nit,
                Nombre_RazonSocial = request.Nombre_RazonSocial,
                Direccion = request.Direccion,
                Telefono = request.Telefono,
                Estado = request.Estado
            };

            var result = await _unitOfWork.Clientes.AddAsync(cliente);
            await _unitOfWork.SaveChangesAsync();
            return result.ClienteId;
        }
    }

    public class UpdateClienteHandler : IRequestHandler<UpdateClienteCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateClienteHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(UpdateClienteCommand request, CancellationToken cancellationToken)
        {
            var cliente = await _unitOfWork.Clientes.GetByIdAsync(request.ClienteId);
            if (cliente == null)
                return false;

            cliente.Cc_Nit = request.Cc_Nit;
            cliente.Nombre_RazonSocial = request.Nombre_RazonSocial;
            cliente.Direccion = request.Direccion;
            cliente.Telefono = request.Telefono;
            cliente.Estado = request.Estado;

            await _unitOfWork.Clientes.UpdateAsync(cliente);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
