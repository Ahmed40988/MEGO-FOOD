
using FluentValidation;

namespace Web.Application.Products.Commands.CreateProduct;

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(150);
        RuleFor(x => x.Price).GreaterThanOrEqualTo(0);
        RuleFor(x => x.MenuCategoryId).NotEmpty();
        RuleFor(x => x.ImagesURL)
    .NotNull()
    .NotEmpty()
    .WithMessage("At least one image is required.");

        RuleForEach(x => x.ImagesURL)
      .Must(file => file.Length > 0)
      .WithMessage("Image cannot be empty.")

      .Must(file => file.Length <= 2 * 1024 * 1024)
      .WithMessage("Image must be less than 2 MB.")

      .Must(file =>
          new[] { ".jpg", ".jpeg", ".png" }
          .Contains(Path.GetExtension(file.FileName).ToLower()))
      .WithMessage("Only .jpg, .jpeg, and .png formats are allowed.");
    }
}
