using EurekaBack.Application.DTOs;
using EurekaBack.Application.Features.Articulos.Commands;
using EurekaBack.Application.Features.Articulos.Queries;
using EurekaBack.Application.Mappers;
using EurekaBack.Domain.Entities;
using EurekaBack.Domain.Interfaces;
using MediatR;

namespace EurekaBack.Application.Features.Articulos.Handlers
{
    public class GetArticulosHandler : IRequestHandler<GetArticulosQuery, IEnumerable<ArticuloDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetArticulosHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<ArticuloDto>> Handle(GetArticulosQuery request, CancellationToken cancellationToken)
        {
            var articulos = await _unitOfWork.Articulos.GetAllAsync();
            return articulos.Select(ArticuloMapper.ToDto);
        }
    }

    public class GetArticuloByIdHandler : IRequestHandler<GetArticuloByIdQuery, ArticuloDto?>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetArticuloByIdHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ArticuloDto?> Handle(GetArticuloByIdQuery request, CancellationToken cancellationToken)
        {
            var articulo = await _unitOfWork.Articulos.GetByIdAsync(request.ArticuloId);
            return articulo != null ? ArticuloMapper.ToDto(articulo) : null;
        }
    }

    public class CreateArticuloHandler : IRequestHandler<CreateArticuloCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateArticuloHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(CreateArticuloCommand request, CancellationToken cancellationToken)
        {
            var articulo = new Articulo
            {
                Codigo = request.Codigo,
                Descripcion = request.Descripcion,
                Costo = request.Costo,
                Porcentaje = request.Porcentaje,
                PrecioSugerido = request.PrecioSugerido,
                Cantidad = request.Cantidad,
                Estado = request.Estado
            };

            var result = await _unitOfWork.Articulos.AddAsync(articulo);
            await _unitOfWork.SaveChangesAsync();
            return result.ArticuloId;
        }
    }

    public class UpdateArticuloHandler : IRequestHandler<UpdateArticuloCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateArticuloHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(UpdateArticuloCommand request, CancellationToken cancellationToken)
        {
            var articulo = await _unitOfWork.Articulos.GetByIdAsync(request.ArticuloId);
            if (articulo == null)
                return false;

            articulo.Codigo = request.Codigo;
            articulo.Descripcion = request.Descripcion;
            articulo.Costo = request.Costo;
            articulo.Porcentaje = request.Porcentaje;
            articulo.PrecioSugerido = request.PrecioSugerido;
            articulo.Cantidad = request.Cantidad;
            articulo.Estado = request.Estado;

            await _unitOfWork.Articulos.UpdateAsync(articulo);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
