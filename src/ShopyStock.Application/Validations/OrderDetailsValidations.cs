using FluentValidation;
using ShopyStock.Application.DTOs.Orders;

namespace ShopyStock.Application.Validations
{
    public class CreateOrderDetialsDtoValidation : AbstractValidator<CreateOrderDetialsDto>
    {
        public CreateOrderDetialsDtoValidation()
        {
            RuleFor(x => x.ProductId)
                .GreaterThan(0).WithMessage("Debes ingresar un identificador valido.");

            RuleFor(x => x.Quantity)
                .GreaterThan(0).WithMessage("Debes pedir al menos un producto.");
        }
    }
}