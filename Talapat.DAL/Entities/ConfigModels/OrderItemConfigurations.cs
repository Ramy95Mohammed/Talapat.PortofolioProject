using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talapat.DAL.Entities.Order_Aggregate;

namespace Talapat.DAL.Entities.ConfigModels
{
    public class OrderItemConfigurations : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.OwnsOne(o => o.ItemOrder, ItemOrder => ItemOrder.WithOwner());
            builder.Property(item => item.Price).HasColumnType("decimal(18,2)");
        }
    }
}
