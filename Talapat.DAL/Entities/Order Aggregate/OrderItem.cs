using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talapat.DAL.Entities.Order_Aggregate
{
    public class OrderItem : BaseEntity
    {
        public OrderItem(ProductItemOrder itemOrder, decimal price, int quantity)
        {
            ItemOrder = itemOrder;
            Price = price;
            Quantity = quantity;
        }
        public OrderItem()
        {
                
        }
        public ProductItemOrder ItemOrder { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }

    }
}
