using EurekaBack.Application.Features.Articulos.Commands;
using FluentValidation;

namespace EurekaBack.Application.Validators
{
    public class CreateArticuloValidator : AbstractValidator<CreateArticuloCommand>
    {
        public CreateArticuloValidator()
        {
            RuleFor(x => x.Codigo)
                .NotEmpty().WithMessage("Code is required")
                .MaximumLength(50).WithMessage("Code cannot exceed 50 characters");

            RuleFor(x => x.Descripcion)
                .NotEmpty().WithMessage("Description is required")
                .MaximumLength(200).WithMessage("Description cannot exceed 200 characters");

            RuleFor(x => x.Costo)
                .GreaterThanOrEqualTo(0).WithMessage("Cost must be greater than or equal to 0");

            RuleFor(x => x.Porcentaje)
                .GreaterThanOrEqualTo(0).WithMessage("Percentage must be greater than or equal to 0");

            RuleFor(x => x.PrecioSugerido)
                .GreaterThanOrEqualTo(0).WithMessage("Suggested price must be greater than or equal to 0");

            RuleFor(x => x.Cantidad)
                .GreaterThanOrEqualTo(0).WithMessage("Quantity must be greater than or equal to 0");
        }
    }

    public class UpdateArticuloValidator : AbstractValidator<UpdateArticuloCommand>
    {
        public UpdateArticuloValidator()
        {
            RuleFor(x => x.ArticuloId)
                .GreaterThan(0).WithMessage("Valid Article ID is required");

            RuleFor(x => x.Codigo)
                .NotEmpty().WithMessage("Code is required")
                .MaximumLength(50).WithMessage("Code cannot exceed 50 characters");

            RuleFor(x => x.Descripcion)
                .NotEmpty().WithMessage("Description is required")
                .MaximumLength(200).WithMessage("Description cannot exceed 200 characters");

            RuleFor(x => x.Costo)
                .GreaterThanOrEqualTo(0).WithMessage("Cost must be greater than or equal to 0");

            RuleFor(x => x.Porcentaje)
                .GreaterThanOrEqualTo(0).WithMessage("Percentage must be greater than or equal to 0");

            RuleFor(x => x.PrecioSugerido)
                .GreaterThanOrEqualTo(0).WithMessage("Suggested price must be greater than or equal to 0");

            RuleFor(x => x.Cantidad)
                .GreaterThanOrEqualTo(0).WithMessage("Quantity must be greater than or equal to 0");
        }
    }
}
