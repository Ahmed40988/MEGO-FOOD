using ErrorOr;

namespace Web.Domain.OrderItems
{
    public static class OrderItemErrors
    {
        public static Error InvalidQuantity =>
            Error.Validation("OrderItem.InvalidQuantity", "Quantity must be greater than 0.");

        public static Error QuantityCannotBeZeroOrNegative =>
            Error.Validation("OrderItem.QuantityTooLow", "Quantity cannot become zero or negative.");

        public static Error InvalidUnitPrice =>
            Error.Validation("OrderItem.InvalidUnitPrice", "Unit price must be greater than 0.");
    }
}
