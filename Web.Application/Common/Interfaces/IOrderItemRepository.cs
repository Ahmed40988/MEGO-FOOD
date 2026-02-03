using Web.Domain.OrderItems;

namespace Web.Application.Common.Interfaces
{
    public interface IOrderItemRepository
    {
        Task AddAsync(orderitem item, CancellationToken cancellationToken = default);
        Task UpdateAsync(orderitem item, CancellationToken cancellationToken = default);
        Task DeleteAsync(orderitem item, CancellationToken cancellationToken = default);

        Task<orderitem?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<orderitem>> GetByOrderIdAsync(Guid orderId, CancellationToken cancellationToken = default);

        Task<bool> ExistsAsync(Guid orderItemId, CancellationToken cancellationToken = default);
        Task RemoveRangeAsync(List<orderitem> items);
    }
}
