using Web.Domain.Orders;

namespace Web.Application.Common.Interfaces
{
    public interface IOrderRepository
    {
        Task AddAsync(Order order, CancellationToken cancellationToken = default);
        Task UpdateAsync(Order order, CancellationToken cancellationToken = default);
        Task DeleteAsync(Order order, CancellationToken cancellationToken = default);

        Task<Order?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<Order>> GetByUserIdAsync(string userId, CancellationToken cancellationToken = default);

        Task<bool> ExistsAsync(Guid orderId, CancellationToken cancellationToken = default);
    }
}

