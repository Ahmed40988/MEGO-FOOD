
using ErrorOr;
using MediatR;

namespace Web.Application.Products.Commands.UpdateProduct;

public class UpdateProductCommandHandler(
    IProductRepository productRepository,
    IFileHelperService fileHelperService,
    IUnitOfWork unitOfWork)
    : IRequestHandler<UpdateProductCommand, ErrorOr<Guid>>
{
    private readonly IFileHelperService _fileHelperService = fileHelperService;

    public async Task<ErrorOr<Guid>> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {
        var product = await productRepository.GetByIdAsync(command.ProductId);

        if(product is null)
            return Error.NotFound("Product.NotFound","Product not found");

        string imageUrl = product.ImageUrl;

        if (command.Image is not null)
        {
            if (!string.IsNullOrEmpty(product.ImageUrl))
                _fileHelperService.DeleteFile(product.ImageUrl, "Products");

            imageUrl = _fileHelperService.UploadFile(command.Image, "Products");
        }

        product.Update(command.Name,command.Description,imageUrl,command.Price,command.UserId);

        await unitOfWork.CommitChangesAsync();
        return product.Id;
    }
}
