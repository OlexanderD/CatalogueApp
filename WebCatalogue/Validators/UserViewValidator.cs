using FluentValidation;
using WebCatalogue.ViewModels;

namespace WebCatalogue.Validators
{
    public class UserViewValidator:AbstractValidator<UserViewModel>
    {

        public UserViewValidator()
        {
            RuleFor(x => x.UserName).NotEmpty()
                .NotNull();

            RuleFor(x => x.Password).NotEmpty()
                .NotNull();
        }

    }
}
