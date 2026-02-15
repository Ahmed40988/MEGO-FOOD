using Web.Application.Common.Interfaces;
using Web.Domain.Orders;
using Web.Infrastructure.Common.Persistence.Data;

namespace Web.Infrastructure.Orders.Persistence
{
    public class OrdersRepository(AppDbContext dbContext) : IOrderRepository
    {
        private readonly AppDbContext _dbContext = dbContext;

        public async Task AddAsync(Order order, CancellationToken cancellationToken = default)
        {
            await _dbContext.AddAsync(order, cancellationToken);
        }

        public Task UpdateAsync(Order order, CancellationToken cancellationToken = default)
        {
            _dbContext.Update(order);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(Order order, CancellationToken cancellationToken = default)
        {
            _dbContext.Remove(order);
            return Task.CompletedTask;
        }

        public async Task<Order?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Orders
                .Include(o => o.Items)
                .Include(o => o.Address)
                .FirstOrDefaultAsync(o => o.Id == id && !o.Deleted, cancellationToken);
        }

        public async Task<IReadOnlyList<Order>> GetByUserIdAsync(
            string userId,
            CancellationToken cancellationToken = default)
        {
            return await _dbContext.Orders
                .AsNoTracking()
                .Include(o => o.Items)
                .Where(o => o.UserId == userId && !o.Deleted)
                .ToListAsync(cancellationToken);
        }

        public async Task<bool> ExistsAsync(Guid orderId, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Orders
                .AsNoTracking()
                .AnyAsync(o => o.Id == orderId && !o.Deleted, cancellationToken);
        }
    }
}


