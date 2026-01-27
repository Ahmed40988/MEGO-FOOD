using ErrorOr;
using Web.Domain.Orders;

namespace Web.Domain.OrderItems
{
    public class orderitem
    {
        public Guid Id { get; private set; }
        public Guid ProductId { get; private set; }
        public Guid OrderId { get; private set; }
        public decimal UnitPrice { get; private set; }
        public decimal Quantity { get; private set; }

        public Product Product { get; private set; } = default!;
        public Order Order { get; private set; } = default!;

        public orderitem(Guid productId, decimal unitPrice, decimal quantity, Guid? id = null)
        {
            if (productId == Guid.Empty)
                throw new ArgumentException("ProductId cannot be empty.");

            if (unitPrice <= 0)
                throw new ArgumentException("Unit price must be greater than 0.");

            if (quantity <= 0)
                throw new ArgumentException("Quantity must be greater than 0.");

            Id = id ?? Guid.NewGuid();
            ProductId = productId;
            UnitPrice = unitPrice;
            Quantity = quantity;
        }

        private orderitem() { }
        public decimal TotalPrice => UnitPrice * Quantity;

        public ErrorOr<Success> IncreaseQuantity(decimal amount)
        {
            if (amount <= 0)
                return OrderItemErrors.InvalidQuantity;

            Quantity += amount;
            return Result.Success;
        }

        public ErrorOr<Success> DecreaseQuantity(decimal amount)
        {
            if (amount <= 0)
                return OrderItemErrors.InvalidQuantity;

            if (Quantity - amount <= 0)
                return OrderItemErrors.QuantityCannotBeZeroOrNegative;

            Quantity -= amount;
            return Result.Success;
        }

        public ErrorOr<Success> UpdateUnitPrice(decimal newPrice)
        {
            if (newPrice <= 0)
                return OrderItemErrors.InvalidUnitPrice;

            UnitPrice = newPrice;
            return Result.Success;
        }
    }
}
