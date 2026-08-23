using Web.Application.Common.File;

namespace Web.Application.Products.Commands.CreateProduct;

public class CreateProductCommandHandler(
    IProductRepository productRepository,
    IUserRepository userRepository,
    IMenuCategoryRepository menuCategoryRepository,
    IFileStorageService fileStorageService,
    IUnitOfWork unitOfWork)
    : IRequestHandler<CreateProductCommand, ErrorOr<Guid>>
{
    private readonly IFileStorageService _fileStorageService = fileStorageService;
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

        var imagePaths = new List<string>();

        if (command.ImagesURL is not null && command.ImagesURL.Any())
        {
            foreach (var image in command.ImagesURL)
            {
                using var stream = image.OpenReadStream();

                var imagePath = await _fileStorageService.SaveFileAsync(
                    stream,
                    image.FileName,
                    "Products-images");

                if (string.IsNullOrWhiteSpace(imagePath))
                    return Error.Validation("ImageUploadFailed", "Failed to upload image");

                imagePaths.Add(imagePath);
            }
        }

        var product = new Product(command.Name,command.Description,imagePaths,command.Price,command.MenuCategoryId);
        var result=category.AddProduct(product);
        if (result.IsError)
            return result.Errors;

        await productRepository.AddAsync(product,cancellationToken);
        await unitOfWork.CommitChangesAsync();

        return product.Id;
    }
}
