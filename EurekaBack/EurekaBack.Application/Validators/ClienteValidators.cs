using EurekaBack.Application.Features.Clientes.Commands;
using FluentValidation;

namespace EurekaBack.Application.Validators
{
    public class CreateClienteValidator : AbstractValidator<CreateClienteCommand>
    {
        public CreateClienteValidator()
        {
            RuleFor(x => x.Cc_Nit)
                .NotEmpty().WithMessage("CC/NIT is required")
                .MaximumLength(20).WithMessage("CC/NIT cannot exceed 20 characters");

            RuleFor(x => x.Nombre_RazonSocial)
                .NotEmpty().WithMessage("Name/Business Name is required")
                .MaximumLength(100).WithMessage("Name/Business Name cannot exceed 100 characters");

            RuleFor(x => x.Direccion)
                .MaximumLength(200).WithMessage("Address cannot exceed 200 characters");

            RuleFor(x => x.Telefono)
                .MaximumLength(20).WithMessage("Phone cannot exceed 20 characters");
        }
    }

    public class UpdateClienteValidator : AbstractValidator<UpdateClienteCommand>
    {
        public UpdateClienteValidator()
        {
            RuleFor(x => x.ClienteId)
                .GreaterThan(0).WithMessage("Valid Client ID is required");

            RuleFor(x => x.Cc_Nit)
                .NotEmpty().WithMessage("CC/NIT is required")
                .MaximumLength(20).WithMessage("CC/NIT cannot exceed 20 characters");

            RuleFor(x => x.Nombre_RazonSocial)
                .NotEmpty().WithMessage("Name/Business Name is required")
                .MaximumLength(100).WithMessage("Name/Business Name cannot exceed 100 characters");

            RuleFor(x => x.Direccion)
                .MaximumLength(200).WithMessage("Address cannot exceed 200 characters");

            RuleFor(x => x.Telefono)
                .MaximumLength(20).WithMessage("Phone cannot exceed 20 characters");
        }
    }
}
