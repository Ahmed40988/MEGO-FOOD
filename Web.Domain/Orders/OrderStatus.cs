namespace Web.Domain.Orders
{
    public enum OrderStatus
    {
        Pending,   // created, waiting for payment/processing
        Paid,      // payment succeeded
        Shipped,   // shipped
        Delivered, // delivered / completed
        Cancelled  // cancelled
    }

}
