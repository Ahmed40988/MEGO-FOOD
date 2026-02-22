using Web.Application.Common;
using Web.Application.Common.Pagination;
using Web.Application.Products.ProductDTO;

public interface IProductRepository
{
    Task<Product?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<PaginatedList<ProductResponse>> GetByCategoryAsync(RequestFilters Filters ,Guid categoryId, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Product>> SearchAsync(string keyword, CancellationToken cancellationToken = default);
    Task<List<Product>> ListBymenuCategoryIdAsync(Guid MenuCategoryId);
    Task<bool> ExistsAsync(Guid productId, CancellationToken cancellationToken = default);
    Task<bool> ExistsByNameAsync(string name, CancellationToken cancellationToken = default); // Added
    Task AddAsync(Product product, CancellationToken cancellationToken = default);
    Task UpdateAsync(Product product, CancellationToken cancellationToken = default);
    Task DeleteAsync(Product product, CancellationToken cancellationToken = default);
    Task RemoveRangeAsync(List<Product> products);
}