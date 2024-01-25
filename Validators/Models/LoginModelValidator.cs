using FluentValidation;
using linhkien_donet.Models.AuthModels;

namespace linhkien_donet.Validators.Models
{
    public class LoginModelValidator:AbstractValidator<LoginModel>
    {
        public LoginModelValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().WithMessage("User Name is required");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required");

        }
    }
}
