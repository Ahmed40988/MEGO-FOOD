namespace Web.Application.MenuCategories.Commands.DeleteMenuCategory
{
    public class DeleteMenuCategoryCommandHandler(IMenuCategoryRepository menuCategoryRepository
        , IUnitOfWork unitOfWork, IProductRepository productRepository) : IRequestHandler<DeleteMenuCategoryCommand, ErrorOr<Deleted>>
    {
        private readonly IMenuCategoryRepository _menuCategoryRepository = menuCategoryRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IProductRepository _productRepository = productRepository;

        public async Task<ErrorOr<Deleted>> Handle(DeleteMenuCategoryCommand command, CancellationToken cancellationToken)
        {
            var menucategory = await _menuCategoryRepository.GetByIdAsync(command.CategoryId, cancellationToken);

            if (menucategory == null)
                return Error.NotFound("MenuCategory.NotFound", "MenuCategory is not found !");

            menucategory.Delete(command.AdminId);//ToDo

            await _menuCategoryRepository.UpdateAsync(menucategory);
            await _unitOfWork.CommitChangesAsync();
            return Result.Deleted;
        }
    }
}
