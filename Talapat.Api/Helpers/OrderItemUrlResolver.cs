using AutoMapper;
using Talapat.Api.DTOS;
using Talapat.DAL.Entities.Order_Aggregate;

namespace Talapat.Api.Helpers
{
    public class OrderItemUrlResolver : IValueResolver<OrderItem, OrderItemDto, string>
    {
        private readonly IConfiguration configuration;

        public OrderItemUrlResolver(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public string Resolve(OrderItem source, OrderItemDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.ItemOrder.PictureUrl))
                return $"{configuration["ApiUrl"]}{source.ItemOrder.PictureUrl}";
            return null;
        }
    }
}
