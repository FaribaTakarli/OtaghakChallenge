
using FluentValidation;

namespace OtaghakChallenge.Application.ProductApplication.Commands.AddProduct;

public class AddProductCommandValidator : AbstractValidator<AddProductCommand>
{
    public AddProductCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotNull()
            .NotEmpty()
            .MaximumLength(200)
            .WithMessage("نام محصول نامعتبر است");

        RuleFor(x => x.Description)
            .NotNull()
            .NotEmpty()
            .MaximumLength(1000)
            .WithMessage("توضیحات محصول نامعتبر است");


        RuleFor(x => x.Status)
            .NotNull()
            .NotEmpty();
    }
}