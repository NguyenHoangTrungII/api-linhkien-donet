using FluentValidation;
using linhkien_donet.Models.AuthModels;

namespace linhkien_donet.Validators.Models
{
    public class RegisterModelValidator:AbstractValidator<RegisterModel>
    {
        public RegisterModelValidator() { 
            RuleFor(x=>x.FirstName).NotEmpty().WithMessage("First Name is require");
            RuleFor(x => x.LastName).NotEmpty().WithMessage("Last Name is require");
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email is require");
            RuleFor(x => x.UserName).NotEmpty().WithMessage("User Name is require");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password is require");
        }
    }
}
