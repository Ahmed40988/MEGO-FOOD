using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Application.Common.Interfaces;
using Web.Domain.OrderItems;
using Web.Infrastructure.Common.Persistence.Data;

namespace Web.Infrastructure.OrderItems.Persistence
{ 
        public class OrderItemsRepository(AppDbContext dbContext) : IOrderItemRepository
        {
            private readonly AppDbContext _dbContext = dbContext;

            public async Task AddAsync(orderitem item, CancellationToken cancellationToken = default)
            {
                await _dbContext.AddAsync(item, cancellationToken);
            }

            public Task UpdateAsync(orderitem item, CancellationToken cancellationToken = default)
            {
                _dbContext.Update(item);
                return Task.CompletedTask;
            }

            public Task DeleteAsync(orderitem item, CancellationToken cancellationToken = default)
            {
                _dbContext.Remove(item);
                return Task.CompletedTask;
            }

            public async Task<orderitem?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
            {
                return await _dbContext.Orderitems
                    .Include(i => i.Product)
                    .FirstOrDefaultAsync(i => i.Id == id, cancellationToken);
            }

            public async Task<IReadOnlyList<orderitem>> GetByOrderIdAsync(
                Guid orderId,
                CancellationToken cancellationToken = default)
            {
                return await _dbContext.Orderitems
                    .AsNoTracking()
                    .Include(i => i.Product)
                    .Where(i => i.OrderId == orderId)
                    .ToListAsync(cancellationToken);
            }

            public async Task<bool> ExistsAsync(Guid orderItemId, CancellationToken cancellationToken = default)
            {
                return await _dbContext.Orderitems
                    .AsNoTracking()
                    .AnyAsync(i => i.Id == orderItemId, cancellationToken);
            }

            public Task RemoveRangeAsync(List<orderitem> items)
            {
                _dbContext.RemoveRange(items);
                return Task.CompletedTask;
            }
        }
    }

