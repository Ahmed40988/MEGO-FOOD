
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

        var imageUrls = product.ImagesURL.ToList();

        foreach (var image in command.ImagesURL!)
        {
            using var stream = image.OpenReadStream();

            var imageUrl = await _fileStorageService.SaveFileAsync(
                stream,
                image.FileName,
                "Products-images");

            imageUrls.Add(imageUrl);
        }

        product.Update(command.Name,command.Description,imageUrls,command.Price,command.UserId);

        await unitOfWork.CommitChangesAsync();
        return product.Id;
    }
}
