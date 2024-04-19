using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talapat.DAL.Entities.Order_Aggregate;
using Talapat.DAL.Entities.RedisEntities;

namespace Talapat.BLL.Interfaces
{
    public interface IPaymentService
    {
        Task<CustomerBasket> CreateOrUpdatePaymentIntent(string basketId);
        Task<Order> UpdateOrderPaymentSucceded(string paymentIntentId);
        Task<DAL.Entities.Order_Aggregate.Order> UpdateOrderPaymentFailed(string paymentIntentId);
    }
}
