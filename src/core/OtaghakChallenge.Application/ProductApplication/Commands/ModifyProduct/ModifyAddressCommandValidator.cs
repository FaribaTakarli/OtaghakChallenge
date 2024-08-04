
using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using OtaghakChallenge.Application.ProductApplication.Commands.AddProduct;

namespace OtaghakChallenge.Application.ProductApplication.Commands.ModifyProduct;
public class ModifyProductCommandValidator : AbstractValidator<ModifyProductCommand>
{
    public ModifyProductCommandValidator(IMapper mapper, AddProductCommandValidator addValidator)
    {
        RuleFor(x => x)
            .MustAsync(async (request, cancellation) =>
            {
                AddProductCommand addCommand = mapper.Map<AddProductCommand>(request);
                ValidationResult validationResult = await addValidator.ValidateAsync(addCommand);

                if (validationResult.IsValid)
                    return true;

                string errors = string.Join(Environment.NewLine, validationResult.Errors.Select(x => x.ErrorMessage).ToArray());
                throw new Exception(errors);
            });
    }
}