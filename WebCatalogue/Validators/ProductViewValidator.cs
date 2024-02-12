using FluentValidation;
using WebCatalogue.ViewModels;

namespace WebCatalogue.Validators
{
    public class ProductViewValidator:AbstractValidator<ProductViewModel>
    {

        public ProductViewValidator()
        {
            RuleFor(x => x.Name).NotEmpty()
                .MaximumLength(50)
                .MinimumLength(3);

            RuleFor(x => x.Description).NotEmpty()
                .MaximumLength(50)
                .MinimumLength(3);

            RuleFor(x => x.Price).NotEmpty();

            RuleFor(x =>x.Categories).NotEmpty();
        }
    }
}
