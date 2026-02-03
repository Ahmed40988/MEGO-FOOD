using ErrorOr;

namespace Web.Domain.Orders
{
    public static class OrderErrors
    {
        public static Error ItemIsNull => Error.Validation(code: "Order.ItemIsNull", description: "Order item cannot be null.");
        public static Error ItemAlreadyExists => Error.Validation(code: "Order.ItemAlreadyExists", description: "The item is already added to the order.");
        public static Error ItemNotFound => Error.Validation(code: "Order.ItemNotFound", description: "Order item not found.");
        public static Error EmptyOrder => Error.Validation(code: "Order.Empty", description: "Order must contain at least one item.");
        public static Error InvalidUser => Error.Validation(code: "Order.InvalidUser", description: "Order must be associated with a valid user.");
        public static Error InvalidAddress => Error.Validation(code: "Order.InvalidAddress", description: "Order has an invalid address.");
        public static Error InvalidDiscount => Error.Validation(code: "Order.InvalidDiscount", description: "Discount must be non-negative.");
        public static Error DiscountTooLarge => Error.Validation(code: "Order.DiscountTooLarge", description: "Discount cannot exceed subtotal.");
        public static Error InvalidStatusTransition => Error.Validation(code: "Order.InvalidStatusTransition", description: "Invalid order status transition.");
    }
}
