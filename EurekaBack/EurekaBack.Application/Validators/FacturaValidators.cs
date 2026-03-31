using EurekaBack.Application.DTOs;
using EurekaBack.Application.Features.Facturas.Commands;
using FluentValidation;

namespace EurekaBack.Application.Validators
{
    public class CreateFacturaValidator : AbstractValidator<CreateFacturaCommand>
    {
        public CreateFacturaValidator()
        {
            RuleFor(x => x.Factura.No)
                .NotEmpty().WithMessage("Invoice number is required")
                .MaximumLength(50).WithMessage("Invoice number cannot exceed 50 characters");

            RuleFor(x => x.Factura.Fecha)
                .NotEmpty().WithMessage("Date is required");

            RuleFor(x => x.Factura.ClienteId)
                .GreaterThan(0).WithMessage("Valid Client ID is required");

            RuleFor(x => x.Factura.lstFacturaDetalleDto)
                .NotEmpty().WithMessage("At least one detail is required");

            RuleForEach(x => x.Factura.lstFacturaDetalleDto).SetValidator(new CreateFacturaDetalleValidator());
        }
    }

    public class CreateFacturaDetalleValidator : AbstractValidator<CreateFacturaDetalleDto>
    {
        public CreateFacturaDetalleValidator()
        {
            RuleFor(x => x.ArticuloId)
                .GreaterThan(0).WithMessage("Valid Article ID is required");

            RuleFor(x => x.Cantidad)
                .GreaterThan(0).WithMessage("Quantity must be greater than 0");
        }
    }
}
