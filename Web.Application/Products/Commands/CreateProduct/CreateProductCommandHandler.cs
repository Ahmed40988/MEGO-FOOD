namespace Web.Application.Products.Commands.CreateProduct;

public class CreateProductCommandHandler(
    IProductRepository productRepository,
    IUserRepository userRepository,
    IMenuCategoryRepository menuCategoryRepository,
    IFileHelperService fileHelperService,
    IUnitOfWork unitOfWork)
    : IRequestHandler<CreateProductCommand, ErrorOr<Guid>>
{
    private readonly IFileHelperService _fileHelperService = fileHelperService;
    private readonly IUnitOfWork _unitOfWork= unitOfWork;
    private readonly IProductRepository _productRepository;
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IMenuCategoryRepository _menuCategoryRepository= menuCategoryRepository;

    public async Task<ErrorOr<Guid>> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(command.AdminId);
        if(user is null)
            return Error.NotFound("Admin.NotFound", "Admin user not found");

        var category = await menuCategoryRepository.GetByIdIncludeProductAsync(command.MenuCategoryId);

        if (category is null)
            return Error.NotFound("MenuCategory.NotFound","MenuCategory not found");

        string imageUrl = string.Empty;

        if (command.Image is not null)
        {
            imageUrl = _fileHelperService.UploadFile(command.Image, "Products");
        }


        var product = new Product(command.Name,command.Description,imageUrl,command.Price,command.MenuCategoryId);
        var result=category.AddProduct(product);
        if (result.IsError)
            return result.Errors;

        await productRepository.AddAsync(product,cancellationToken);
        await unitOfWork.CommitChangesAsync();

        return product.Id;
    }
}
