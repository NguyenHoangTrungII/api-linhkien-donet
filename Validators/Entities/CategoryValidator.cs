using FluentValidation;
using linhkien_donet.Entities;

namespace linhkien_donet.Validators.Entities
{
    public class CategoryValidator: AbstractValidator<Category>
    {
        public CategoryValidator()
        {
            RuleFor(category => category.Name).NotEmpty().WithMessage("Category'S Name is required");
        }
                
    }
}
