using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
