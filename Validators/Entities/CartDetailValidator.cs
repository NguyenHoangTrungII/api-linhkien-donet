using FluentValidation;
using linhkien_donet.Entities;

namespace linhkien_donet.Validators.Entities
{
    public class CartDetailValidator: AbstractValidator<CartDetail>
    {
        public CartDetailValidator() {
            RuleFor(cartDetail => cartDetail.Quantity).NotEmpty().WithMessage("Quantity is required");
            RuleFor(cartDetail => cartDetail.CartId).NotEmpty().WithMessage("CartId is required");
            RuleFor(cartDetail => cartDetail.ProductId).NotEmpty().WithMessage("ProductId is required");

        }
    }
}
