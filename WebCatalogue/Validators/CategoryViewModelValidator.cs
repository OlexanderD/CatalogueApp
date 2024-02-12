using FluentValidation;
using System.Data;
using WebCatalogue.ViewModels;

namespace WebCatalogue.Validators
{
    public class CategoryViewModelValidator: AbstractValidator<CategoryViewModel>
    {
        public CategoryViewModelValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(50)
                .MinimumLength(3);

        }
    }
}
