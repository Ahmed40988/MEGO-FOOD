namespace Web.Application.Products.Commands.DeleteProduct
{
    public class DeleteProductCommandHandler(IProductRepository productRepository, IUnitOfWork unitOfWork) : IRequestHandler<DeleteProductCommand, ErrorOr<Deleted>>
    {
        private readonly IProductRepository _productRepository = productRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<ErrorOr<Deleted>> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetByIdAsync(command.productId);
            if (product == null)
                return Error.NotFound(description: "Product Is not found !");

            product.Delete("AdminID"); // TOdo

            await _productRepository.UpdateAsync(product);
            await _unitOfWork.CommitChangesAsync();
            return Result.Deleted;
        }
    }
}
