using FluentValidation;
using ShopyStock.Application.DTOs.Products;

namespace ShopyStock.Application.Validations
{
    public class CreateProductDtoValidatior : AbstractValidator<CreateProductDto>
    {
        public CreateProductDtoValidatior()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Debes ingresar un nombre para el producto.")
                .MinimumLength(4).WithMessage("El nombre debe contener al menos 4 caracteres.")
                .MaximumLength(120).WithMessage("El nombre debe contener menos de 120 caracteres.");

            RuleFor(x => x.Price)
                .GreaterThan(1).WithMessage("El precio debe ser mayor a 1.");
            
            RuleFor(x => x.Stock)
                .GreaterThanOrEqualTo(0).WithMessage("El stock debe ser igual o mayor a 0.");

        }
    }
}