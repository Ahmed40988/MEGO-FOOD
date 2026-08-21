
using ErrorOr;
using MediatR;
using System.Reflection;
using Web.Application.Common.File;

namespace Web.Application.Products.Commands.UpdateProduct;

public class UpdateProductCommandHandler(
    IProductRepository productRepository,
    IFileStorageService fileStorageService,
    IUnitOfWork unitOfWork)
    : IRequestHandler<UpdateProductCommand, ErrorOr<Guid>>
{
    private readonly IFileStorageService _fileStorageService = fileStorageService;

    public async Task<ErrorOr<Guid>> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {
        var product = await productRepository.GetByIdAsync(command.ProductId);

        if(product is null)
            return Error.NotFound("Product.NotFound","Product not found");

        string imageUrl = product.ImageUrl;

        if (command.Image is not null)
        {
            using var stream = command.Image.OpenReadStream();

            imageUrl = await _fileStorageService.UpdateFileAsync(
                stream,
                command.Image.FileName,
                product.ImageUrl,
                "Products-images");
            if (string.IsNullOrWhiteSpace(imageUrl))
            {
                return Error.Failure("FileUpload.Failed", "Failed to upload the image file.");
            }
        }

        product.Update(command.Name,command.Description,imageUrl,command.Price,command.UserId);

        await unitOfWork.CommitChangesAsync();
        return product.Id;
    }
}
