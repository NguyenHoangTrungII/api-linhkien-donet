using FluentValidation;
using linhkien_donet.Models.AuthModels;

namespace linhkien_donet.Validators.Models
{
    public class UpdatePermissionValidator:AbstractValidator<UpdatePermission>
    {
        public UpdatePermissionValidator() { 
            RuleFor(x=>x.UserName).NotEmpty().WithMessage("User name is required");
        }
    }
}
