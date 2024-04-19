using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talapat.DAL.Entities.Order_Aggregate;

namespace Talapat.BLL.Specifications.OrderSpecification
{
    public class OrderWithItemsByPaymentIntentSpecification : BaseSpecification<Order>
    {
        public OrderWithItemsByPaymentIntentSpecification(string pymentIntentId):base(o=>o.PaymentIntentId == pymentIntentId)
        {
                
        }

    }
}
