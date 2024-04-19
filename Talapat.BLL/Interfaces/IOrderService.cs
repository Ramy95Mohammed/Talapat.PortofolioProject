using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talapat.DAL.Entities.Order_Aggregate;

namespace Talapat.BLL.Interfaces
{
    public interface IOrderService
    {
        public Task<Order> CreateOrderAsync(string BuyerEmail , string BasketId ,int deliveryMethodId,Address ShipToAddress);
        public Task<IReadOnlyList<Order>> GetOrderForUserAsync(string BuyerEmail);
        public Task<Order> GetOrderByIdForUser(int OrderId, string BuyerEmail);
        public Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodAsync();
    }
}
