using FluentValidation;
using linhkien_donet.Entities;

namespace linhkien_donet.Validators.Entities
{
    public class BrandValidator: AbstractValidator<Brand>
    {
        public BrandValidator()
        {
            RuleFor(brand => brand.Name).NotEmpty().WithMessage("Brand's Name is required");
        }
    }
}
