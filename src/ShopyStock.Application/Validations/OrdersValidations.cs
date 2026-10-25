using FluentValidation;
using ShopyStock.Application.DTOs.Orders;

namespace ShopyStock.Application.Validations
{
    public class CreateOrderDtoValidation : AbstractValidator<CreateOrderDto>
    {
        public CreateOrderDtoValidation()
        {
            RuleFor(x => x.Details)
                .NotNull().WithMessage("Debes de ingresar los productos que se pediran.");

            RuleForEach(x => x.Details)
                .SetValidator(new CreateOrderDetialsDtoValidation());

            RuleFor(x => x.Status)
                .IsInEnum().WithMessage("El status ingresado no es valido.");
        }
    }
}

