namespace Web.Application.Products.Queries.listProductQuery
{
    public class listProductQueryHandler(IMenuCategoryRepository menuCategoryRepository, IProductRepository productRepository) : IRequestHandler<listProductQuery, ErrorOr<List<Product>>>
    {
        private readonly IMenuCategoryRepository _menuCategoryRepository = menuCategoryRepository;
        private readonly IProductRepository _productRepository = productRepository;

        public async Task<ErrorOr<List<Product>>> Handle(listProductQuery query, CancellationToken cancellationToken)
        {
            if (!await _menuCategoryRepository.ExistsAsync(query.MenuCategoryId))
            {
                return Error.NotFound(description: "Menu Category not found");
            }

            return await _productRepository.ListBymenuCategoryIdAsync(query.MenuCategoryId);
        }
    }
}
