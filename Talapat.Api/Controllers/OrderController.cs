using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Talapat.Api.DTOS;
using Talapat.Api.Errors;
using Talapat.BLL.Interfaces;
using Talapat.DAL.Entities.Order_Aggregate;

namespace Talapat.Api.Controllers
{
    [Authorize]
    public class OrderController : BaseApiController
    {
        private readonly IOrderService orderService;
        private readonly IMapper mapper;

        public OrderController(IOrderService orderService,IMapper mapper)
        {
            this.orderService = orderService;
            this.mapper = mapper;
        }
      
        [HttpPost]
        public async Task<ActionResult<Order>> CreateOrder(OrderDto orderDto)
        {
            var BuyerEmail = User.FindFirstValue(ClaimTypes.Email);
            var OrderAddress = mapper.Map<AddressDto, Address>(orderDto.ShipToAddress);
            var result = await orderService.CreateOrderAsync(BuyerEmail, orderDto.BasketId, orderDto.deliveryMethodId, OrderAddress);
            if (result == null)
                return BadRequest(new ApiResponse(400, "Ann Error Occured During Creating The Order"));
            return Ok(result);
        }
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<OrderToReturnDto>>> GetOrdersForUser()
        {
            var BuyerEmail = User.FindFirstValue(ClaimTypes.Email);
            var Orders =await orderService.GetOrderForUserAsync(BuyerEmail);
            var ordersToReturnDto = mapper.Map<IReadOnlyList< Order>,IReadOnlyList< OrderToReturnDto>>(Orders);
            return Ok(ordersToReturnDto);
        }
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderToReturnDto>> GetOrderForUser(int id)
        {
            var BuyerEmail = User.FindFirstValue(ClaimTypes.Email);
            var order = await orderService.GetOrderByIdForUser(id, BuyerEmail);
            var orderToReturnDto = mapper.Map<Order, OrderToReturnDto>(order);
            return Ok(orderToReturnDto);
        }
        [Authorize]
        [HttpGet("deliveryMethod")]
        public async Task<ActionResult<IReadOnlyList< DeliveryMethod>>> GetDeliveryMethod()
        {
             return Ok( await orderService.GetDeliveryMethodAsync());
        }
    }
}
