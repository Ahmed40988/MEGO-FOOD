using ErrorOr; // افتراض وجود مكتبة ErrorOr
using Web.Domain.Addresses;
using Web.Domain.BaseModels;
using Web.Domain.OrderItems;
using Web.Domain.Users;

namespace Web.Domain.Orders
{
    public class Order : BaseModel
    {
        public Guid Id { get; private set; }
        public string UserId { get; private set; } = string.Empty;
        public AppUser User { get; private set; } = default!;

        public int AddressId { get; private set; }
        public Address Address { get; private set; } = default!;

        public OrderStatus Status { get; private set; }
        public decimal Subtotal { get; private set; }
        public decimal Discount { get; private set; }
        public decimal Tax { get; private set; }
        public decimal Total { get; private set; }

        private readonly List<orderitem> _items = new();
        public IReadOnlyCollection<orderitem> Items => _items.AsReadOnly();

        public Order(string userId, int addressId, IEnumerable<orderitem>? items = null, Guid? id = null)
        {
            if (string.IsNullOrWhiteSpace(userId))
                throw new ArgumentException("UserId is required", nameof(userId));

            Id = id ?? Guid.NewGuid();
            UserId = userId;
            AddressId = addressId;
            Status = OrderStatus.Pending;

            if (items != null)
                _items.AddRange(items);

            RecalculateTotals();
        }

        private Order() { }
        public ErrorOr<Success> AddItem(orderitem item)
        {
            if (item is null)
                return OrderErrors.ItemIsNull;

            if (_items.Any(i => i.ProductId == item.ProductId))
                return OrderErrors.ItemAlreadyExists;

            _items.Add(item);
            RecalculateTotals();
            return Result.Success;
        }


        public ErrorOr<Success> RemoveItem(Guid orderItemId)
        {
            var existing = _items.FirstOrDefault(i => i.Id == orderItemId);
            if (existing is null)
                return OrderErrors.ItemNotFound;

            _items.Remove(existing);
            RecalculateTotals();
            return Result.Success;
        }


        public ErrorOr<Success> SetAddress(int addressId)
        {
            if (addressId <= 0)
                return OrderErrors.InvalidAddress;

            AddressId = addressId;
            return Result.Success;
        }


        public ErrorOr<Success> ApplyDiscount(decimal discountAmount)
        {
            if (discountAmount < 0)
                return OrderErrors.InvalidDiscount;

            if (discountAmount > Subtotal)
                return OrderErrors.DiscountTooLarge;

            Discount = discountAmount;
            RecalculateTotals();
            return Result.Success;
        }

        public ErrorOr<Success> ChangeStatus(OrderStatus newStatus)
        {
            if (Status == newStatus)
                return Result.Success;

            var allowedTransitions = new Dictionary<OrderStatus, HashSet<OrderStatus>>
            {
                [OrderStatus.Pending] = new HashSet<OrderStatus> { OrderStatus.Paid, OrderStatus.Cancelled },
                [OrderStatus.Paid] = new HashSet<OrderStatus> { OrderStatus.Shipped, OrderStatus.Cancelled },
                [OrderStatus.Shipped] = new HashSet<OrderStatus> { OrderStatus.Delivered },
                [OrderStatus.Delivered] = new HashSet<OrderStatus>(), // terminal
                [OrderStatus.Cancelled] = new HashSet<OrderStatus>()  // terminal
            };

            if (!allowedTransitions.TryGetValue(Status, out var allowed) || !allowed.Contains(newStatus))
                return OrderErrors.InvalidStatusTransition;

            Status = newStatus;
            return Result.Success;
        }

        public ErrorOr<Success> ValidateBeforeCheckout()
        {
            if (!_items.Any())
                return OrderErrors.EmptyOrder;

            if (string.IsNullOrWhiteSpace(UserId))
                return OrderErrors.InvalidUser;

            if (AddressId <= 0)
                return OrderErrors.InvalidAddress;

            return Result.Success;
        }

        private void RecalculateTotals()
        {
            Subtotal = _items.Sum(i => i.UnitPrice * i.Quantity);

            Tax = Math.Round(Subtotal * 0.10m, 2);
            Total = Subtotal + Tax - Discount;
            if (Total < 0) Total = 0;
        }
    }
}
