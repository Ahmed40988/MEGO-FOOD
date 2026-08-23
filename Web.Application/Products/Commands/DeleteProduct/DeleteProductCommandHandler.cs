
using ErrorOr;
using MediatR;
using Web.Application.Common.File;

namespace Web.Application.Products.Commands.DeleteProduct;

public class DeleteProductCommandHandler(
    IProductRepository productRepository,
    IFileStorageService fileStorageService,
    IUnitOfWork unitOfWork)
    : IRequestHandler<DeleteProductCommand, ErrorOr<Success>>
{
    private readonly IProductRepository _productRepository = productRepository;
    private readonly IFileStorageService _fileStorageService = fileStorageService;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<ErrorOr<Success>> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
    {
        var product = await productRepository.GetByIdAsync(command.ProductId);

        if(product is null)
            return Error.NotFound("Product.NotFound","Product not found");

        if (product.ImagesURL is not null && product.ImagesURL.Any())
        {
            foreach (var image in product.ImagesURL)
            {
                await _fileStorageService.DeleteFileAsync(image);
            }
        }

        product.Delete(command.UserId);
        await productRepository.UpdateAsync(product);
        await unitOfWork.CommitChangesAsync();
        return Result.Success;
    }
}
