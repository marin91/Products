using FluentValidation;
using Products.Api.Models;

namespace Products.Api.Validators
{
    public class ProductValidator : AbstractValidator<Product> 
    {
        public ProductValidator() 
        { 
            RuleFor(product => product.Id).GreaterThan(0).WithMessage("The product Id provided isn't supported.");
            RuleFor(product => product.Price).GreaterThan(0).WithMessage("The product price must be greater than 0.");
            RuleFor(product => product.Quantity).GreaterThanOrEqualTo(0).WithMessage("The product quantity can't be negative.");
        }
    }
}
