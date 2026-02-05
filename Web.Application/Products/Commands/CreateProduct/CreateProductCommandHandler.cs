namespace Web.Application.Products.Commands.CreateProduct;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ErrorOr<Guid>>
{
    private readonly IProductRepository _repository;

    public CreateProductCommandHandler(IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task<ErrorOr<Guid>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        // Check if a product with the same name already exists
        if (await _repository.ExistsByNameAsync(request.Name, cancellationToken))
        {
            return Error.Conflict("Product.DuplicateName", "A product with the same name already exists.");
        }

        // Create the product using the constructor
        var product = new Product(
            request.Name,
            request.Description,
            request.ImageUrl,
            request.Price,
            request.MenuCategoryId
        );

        await _repository.AddAsync(product, cancellationToken);

        return product.Id;
    }
}
