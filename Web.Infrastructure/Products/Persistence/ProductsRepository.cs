using Web.Application.Common.Interfaces;
using Web.Infrastructure.Common.Persistence.Data;

namespace Web.Infrastructure.Products.Persistence
{
    public class ProductsRepository(
        AppDbContext dbContext,
        IFuzzySearchRepository fuzzySearchRepository)
        : IProductRepository
    {
        private readonly AppDbContext _dbContext = dbContext;
        private readonly IFuzzySearchRepository _fuzzySearchRepository = fuzzySearchRepository;

        public async Task AddAsync(Product product, CancellationToken cancellationToken = default)
        {
            await _dbContext.AddAsync(product, cancellationToken);
        }

        public Task DeleteAsync(Product product, CancellationToken cancellationToken = default)
        {
            _dbContext.Remove(product);
            return Task.CompletedTask;
        }

        public async Task<bool> ExistsAsync(Guid productId, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Products
                .AsNoTracking()
                .AnyAsync(p => p.Id == productId, cancellationToken);
        }

        public async Task<IReadOnlyList<Product>> GetByCategoryAsync(
            Guid categoryId,
            CancellationToken cancellationToken = default)
        {
            return await _dbContext.Products
                .AsNoTracking()
                .Where(p => p.MenuCategoryId == categoryId)
                .ToListAsync(cancellationToken);
        }

        public async Task<Product?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Products
                .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
        }

        public async Task<List<Product>> ListBymenuCategoryIdAsync(Guid menuCategoryId)
        {
            return await _dbContext.Products
                .AsNoTracking()
                .Where(p => p.MenuCategoryId == menuCategoryId)
                .ToListAsync();
        }

        public Task RemoveRangeAsync(List<Product> products)
        {
            _dbContext.RemoveRange(products);
            return Task.CompletedTask;
        }

        public async Task<IReadOnlyList<Product>> SearchAsync(
            string keyword,
            CancellationToken cancellationToken = default)
        {
            var products = await _dbContext.Products
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            var result = products
                .Select(p => new
                {
                    Product = p,
                    Score = _fuzzySearchRepository.CalculateSimilarity(keyword, p.Name)
                })
                .Where(x => x.Score >= 80)
                .OrderByDescending(x => x.Score)
                .Select(x => x.Product)
                .ToList();

            return result;
        }

        public Task UpdateAsync(Product product, CancellationToken cancellationToken = default)
        {
            _dbContext.Update(product);
            return Task.CompletedTask;
        }
    }
}
