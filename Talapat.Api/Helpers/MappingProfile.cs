using AutoMapper;
using Talapat.Api.DTOS;
using Talapat.DAL.Entities;
using Talapat.DAL.Entities.Order_Aggregate;
using Talapat.DAL.Entities.RedisEntities;

namespace Talapat.Api.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductToReturnDto>()
                .ForMember(d=>d.ProductType,o=>o.MapFrom(s=>s.ProductType.Name))
                .ForMember(d=>d.ProductBrand,o=>o.MapFrom(s=>s.ProductBrand.Name))
                .ForMember(d=>d.PictureUrl,o=>o.MapFrom<ProductPictureUrlResolver>());
            CreateMap<DAL.Entities.IdentityEntities.Address, AddressDto>().ReverseMap();
            CreateMap<AddressDto, DAL.Entities.Order_Aggregate.Address>();
            CreateMap<CustomerBasketDto, CustomerBasket>();
            CreateMap<BasketItemDto, BasketItem>();

            CreateMap<Order, OrderToReturnDto>()
                .ForMember(d => d.DeliveryMethod, o => o.MapFrom(s => s.DeliveryMethod.ShortName))
                .ForMember(d => d.DeliveryCost, o => o.MapFrom(s => s.DeliveryMethod.Cost));
            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(d => d.ProductId, o => o.MapFrom(s => s.ItemOrder.ProductId))
                .ForMember(d => d.ProductName, o => o.MapFrom(s => s.ItemOrder.ProductName))
                .ForMember(d => d.PictureUrl, o => o.MapFrom(s => s.ItemOrder.PictureUrl))
                .ForMember(d=>d.PictureUrl,o=>o.MapFrom<OrderItemUrlResolver>());

        }
    }
}
