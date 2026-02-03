namespace Web.Application.Products.Commands.CreateProduct
{
    public class CreateProductCommandHandler(IMenuCategoryRepository menuCategoryRepository, IUnitOfWork unitOfWork) : IRequestHandler<CreateProductCommand, ErrorOr<Product>>
    {
        private readonly IMenuCategoryRepository _menuCategoryRepository = menuCategoryRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<ErrorOr<Product>> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            var menucategory = await _menuCategoryRepository.GetByIdAsync(command.menuCategoryId);
            if (menucategory == null)
                return Error.NotFound(description: "Menucategory For this Product is not found");

            var product = new Product
                (
                command.name
               , command.description
               , command.imageUrl
               , command.price
               , command.menuCategoryId
               );
            var addProductResult = menucategory.AddProduct(product);

            if (addProductResult.IsError)
                return addProductResult.Errors;


            await _menuCategoryRepository.UpdateAsync(menucategory);
            await _unitOfWork.CommitChangesAsync();

            return product;
        }
    }
}
