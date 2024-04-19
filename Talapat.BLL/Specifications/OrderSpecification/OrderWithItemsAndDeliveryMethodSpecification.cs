using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talapat.DAL.Entities.Order_Aggregate;

namespace Talapat.BLL.Specifications.OrderSpecification
{
    public class OrderWithItemsAndDeliveryMethodSpecification : BaseSpecification<Order>
    {
        public OrderWithItemsAndDeliveryMethodSpecification(string buyerEmail):base(o=>o.BuyerEmail==buyerEmail)
        {
            AddInclude(o => o.DeliveryMethod);
            AddInclude(o => o.Items);
            AddOrderByDescinding(o => o.OrderDate);
        }
        public OrderWithItemsAndDeliveryMethodSpecification(int orderId, string buyerEmail):base(o=>o.Id == orderId && o.BuyerEmail==buyerEmail)
        {
            AddInclude(o => o.DeliveryMethod);
            AddInclude(o => o.Items);            
        }
    }
}
