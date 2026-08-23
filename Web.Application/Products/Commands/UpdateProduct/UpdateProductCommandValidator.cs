
using FluentValidation;

namespace Web.Application.Products.Commands.UpdateProduct;

public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(x => x.ProductId).NotEmpty();
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Price).GreaterThanOrEqualTo(0);


        RuleFor(x => x.ImagesURL)
             .Must(images => images == null || images.Count <= 10)
             .WithMessage("Maximum 10 images are allowed.");

        When(x => x.ImagesURL is not null && x.ImagesURL.Any(), () =>
        {
            RuleForEach(x => x.ImagesURL)
                .NotNull()
                .Must(file => file.Length > 0)
                .WithMessage("Image cannot be empty.")

                .Must(file => file.Length <= 2 * 1024 * 1024)
                .WithMessage("Image must be less than 2 MB.")

                .Must(file =>
                    new[] { ".jpg", ".jpeg", ".png" }
                    .Contains(Path.GetExtension(file.FileName).ToLowerInvariant()))
                .WithMessage("Only .jpg, .jpeg, and .png formats are allowed.");
        });

    }
}
