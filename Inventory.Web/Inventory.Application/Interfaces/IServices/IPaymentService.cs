using Stripe.Checkout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Application.Interfaces.IServices
{
    public interface IPaymentService
    {
        Session CreateCheckoutSession(decimal amount, int orderId);
    }
}
