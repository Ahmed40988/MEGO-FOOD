using ErrorOr;
using System.Linq.Dynamic.Core;
using Web.Application.Common;
using Web.Application.Common.Interfaces;
using Web.Application.Common.Pagination;
using Web.Application.Products.ProductDTO;
using Web.Application.Restaurants.Contracts;
using Web.Domain.BaseCategories;
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
                .AnyAsync(p => p.Id == productId&&!p.Deleted, cancellationToken);
        }

        public async Task<bool> ExistsByNameAsync(string name, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Products
                .AnyAsync(p => p.Name == name && !p.Deleted, cancellationToken);
        }

        public async Task<PaginatedList<ProductResponse>> GetByCategoryAsync(RequestFilters filters,
            Guid?categoryId,
            CancellationToken cancellationToken = default)
        
            {
                var query = _dbContext.Products
                    .Where(b => !b.Deleted)
                    .AsNoTracking()
                    .AsQueryable();

            if (categoryId.HasValue)
            {
                query = query.Where(b => b.MenuCategoryId == categoryId.Value);
            }

            if(filters.TopRaing==true)
            {
                query = query.Where(b => b.Rating>=4).OrderByDescending(x=>x.Rating).Take(30);
            }

            if (!string.IsNullOrEmpty(filters.SearchValue))
                {
                    query = query.Where(b =>
                        b.Name.Contains(filters.SearchValue) ||
                        b.Description.Contains(filters.SearchValue));
                }


                var totalCount = await query.CountAsync(cancellationToken);


                if (!string.IsNullOrEmpty(filters.SortColumn))
                {
                    query = query.OrderBy($"{filters.SortColumn} {filters.SortDirection}");
                }
                else
                {
                    query = query.OrderBy(x => x.Name);
                }

                if (filters.PageNumber > 0 && filters.PageSize > 0)
                {
                    var skip = (filters.PageNumber - 1) * filters.PageSize;
                    query = query.Skip(skip).Take(filters.PageSize);
                }

                var items = await query
                    .Select(x => new ProductResponse(
                        x.Id,
                        x.Name,
                        x.Description
                        ,x.ImageUrl
                        ,x.Price
                        ,x.Rating
                        ))
                    .ToListAsync(cancellationToken);

                return new PaginatedList<ProductResponse>
                    (items,
                    filters.PageNumber,
                    totalCount,
                    filters.PageSize);

            }

        public async Task<Product?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Products
                .FirstOrDefaultAsync(p => p.Id == id && !p.Deleted, cancellationToken);
        }

        public async Task<List<Product>> ListBymenuCategoryIdAsync(Guid menuCategoryId)
        {
            return await _dbContext.Products
                .AsNoTracking()
                .Where(p => p.MenuCategoryId == menuCategoryId && !p.Deleted)
                .ToListAsync();
        }

        public Task RemoveRangeAsync(List<Product> products)
        {
            _dbContext.RemoveRange(products);
            return Task.CompletedTask;
        }

        public async Task<ErrorOr<List<ProductResponse>>> SearchAsync(
            string keyword,
            CancellationToken cancellationToken = default)
        {
            var products = await _dbContext.Products
                .AsNoTracking()
                .Where(p => !p.Deleted &&
                    EF.Functions.Like(p.Name, $"%{keyword}%"))
                .Take(50)
                .ToListAsync(cancellationToken);
            if(products.Count == 0)
                return Error.NotFound("Products.NotFound","No Found Products With This Keyword Search") ;

            var result= products
                .Select(p => new
                {
                    Product = p,
                    Score = _fuzzySearchRepository.CalculateSimilarity(
                        keyword,
                        p.Name)
                })
                .OrderByDescending(x => x.Score)
                .Select(x => x.Product)
                .Take(20)
                .ToList();

            return result.Select(p => new ProductResponse(
                p.Id,
                p.Name,
                p.Description,
                p.ImageUrl,
                p.Price,
                p.Rating)).ToList();
        }

        public Task UpdateAsync(Product product, CancellationToken cancellationToken = default)
        {
            _dbContext.Update(product);
            return Task.CompletedTask;
        }


    }
}
