using EurekaBack.Application.DTOs;
using EurekaBack.Application.Features.Facturas.Commands;
using EurekaBack.Application.Features.Facturas.Queries;
using EurekaBack.Application.Mappers;
using EurekaBack.Domain.Entities;
using EurekaBack.Domain.Interfaces;
using MediatR;

namespace EurekaBack.Application.Features.Facturas.Handlers
{
    public class GetFacturasHandler : IRequestHandler<GetFacturasQuery, IEnumerable<FacturaDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetFacturasHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<FacturaDto>> Handle(GetFacturasQuery request, CancellationToken cancellationToken)
        {
            var facturas = await _unitOfWork.Facturas.GetAllAsync();
            return facturas.Select(FacturaMapper.ToDto);
        }
    }

    public class GetFacturasByDateRangeHandler : IRequestHandler<GetFacturasByDateRangeQuery, IEnumerable<FacturaDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetFacturasByDateRangeHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<FacturaDto>> Handle(GetFacturasByDateRangeQuery request, CancellationToken cancellationToken)
        {
            var facturas = await _unitOfWork.Facturas.GetByDateRangeAsync(request.FechaInicial, request.FechaFinal);
            return facturas.Select(FacturaMapper.ToDto);
        }
    }

    public class GetFacturaByIdHandler : IRequestHandler<GetFacturaByIdQuery, FacturaDto?>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetFacturaByIdHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<FacturaDto?> Handle(GetFacturaByIdQuery request, CancellationToken cancellationToken)
        {
            var factura = await _unitOfWork.Facturas.GetByIdWithDetailsAsync(request.FacturaId);
            return factura != null ? FacturaMapper.ToDto(factura) : null;
        }
    }

    public class GetFacturaDetalleHandler : IRequestHandler<GetFacturaDetalleQuery, IEnumerable<FacturaDetalleDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetFacturaDetalleHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<FacturaDetalleDto>> Handle(GetFacturaDetalleQuery request, CancellationToken cancellationToken)
        {
            var factura = await _unitOfWork.Facturas.GetByIdWithDetailsAsync(request.FacturaId);
            if (factura == null || factura.lstFacturaDetalle == null)
                return Enumerable.Empty<FacturaDetalleDto>();

            return factura.lstFacturaDetalle.Select(FacturaDetalleMapper.ToDto);
        }
    }

    public class CreateFacturaHandler : IRequestHandler<CreateFacturaCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateFacturaHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(CreateFacturaCommand request, CancellationToken cancellationToken)
        {
            await _unitOfWork.BeginTransactionAsync();

            try
            {
                var factura = new Factura
                {
                    No = request.Factura.No,
                    Fecha = request.Factura.Fecha,
                    ClienteId = request.Factura.ClienteId,
                    Total = 0
                };

                var detalles = new List<FacturaDetalle>();
                decimal total = 0;

                foreach (var detalleDto in request.Factura.lstFacturaDetalleDto)
                {
                    var articulo = await _unitOfWork.Articulos.GetByIdAsync(detalleDto.ArticuloId);
                    if (articulo == null)
                    {
                        await _unitOfWork.RollbackTransactionAsync();
                        throw new Exception($"Article with ID {detalleDto.ArticuloId} not found");
                    }

                    if (articulo.Cantidad < detalleDto.Cantidad)
                    {
                        await _unitOfWork.RollbackTransactionAsync();
                        throw new Exception($"Insufficient stock for article {articulo.Codigo}. Available: {articulo.Cantidad}, Requested: {detalleDto.Cantidad}");
                    }

                    var subTotal = articulo.PrecioSugerido * detalleDto.Cantidad;
                    total += subTotal;

                    detalles.Add(new FacturaDetalle
                    {
                        ArticuloId = detalleDto.ArticuloId,
                        Cantidad = detalleDto.Cantidad,
                        Precio = articulo.PrecioSugerido,
                        SubTotal = subTotal
                    });

                    await _unitOfWork.Articulos.UpdateStockAsync(articulo.ArticuloId, detalleDto.Cantidad);
                }

                factura.Total = total;
                factura.lstFacturaDetalle = detalles;

                var result = await _unitOfWork.Facturas.AddAsync(factura);
                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitTransactionAsync();

                return result.FacturaId;
            }
            catch
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }
    }
}
