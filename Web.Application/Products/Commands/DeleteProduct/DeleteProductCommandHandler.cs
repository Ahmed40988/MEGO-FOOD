
using ErrorOr;
using MediatR;

namespace Web.Application.Products.Commands.DeleteProduct;

public class DeleteProductCommandHandler(
    IProductRepository productRepository,
    IFileHelperService fileHelperService,
    IUnitOfWork unitOfWork)
    : IRequestHandler<DeleteProductCommand, ErrorOr<Success>>
{
    private readonly IProductRepository _productRepository = productRepository;
    private readonly IFileHelperService _fileHelperService = fileHelperService;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<ErrorOr<Success>> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
    {
        var product = await productRepository.GetByIdAsync(command.ProductId);

        if(product is null)
            return Error.NotFound("Product.NotFound","Product not found");

        if (!string.IsNullOrEmpty(product.ImageUrl))
            _fileHelperService.DeleteFile(product.ImageUrl, "Products");

        product.Delete(command.UserId);
        await productRepository.UpdateAsync(product);
        await unitOfWork.CommitChangesAsync();
        return Result.Success;
    }
}
