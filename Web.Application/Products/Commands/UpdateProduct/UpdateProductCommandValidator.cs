
using FluentValidation;

namespace Web.Application.Products.Commands.UpdateProduct;

public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(x => x.ProductId).NotEmpty();
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Price).GreaterThanOrEqualTo(0);

        RuleFor(x => x.Image)
       .NotNull().WithMessage("Profile image is required.")
       .Must(file => file.Length > 0).WithMessage("Profile image cannot be empty.")
       .Must(file => file.Length <= 2 * 1024 * 1024).WithMessage("Profile image must be less than 2 MB.")
       .Must(file => new[] { ".jpg", ".jpeg", ".png" }
           .Contains(Path.GetExtension(file.FileName).ToLower()))
       .WithMessage("Only .jpg, .jpeg, and .png formats are allowed.");
    }
}

