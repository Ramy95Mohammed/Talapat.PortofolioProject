using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talapat.Api.DTOS;
using Talapat.BLL.Interfaces;
using Talapat.DAL.Entities.RedisEntities;

namespace Talapat.Api.Controllers
{
   
    public class BasketController : BaseApiController
    {
        private readonly IBasketRepository basketRepository;
        private readonly IMapper mapper;

        public BasketController(IBasketRepository basketRepository, IMapper mapper)
        {
            this.basketRepository = basketRepository;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<CustomerBasket>>GetBasket(string baskedId)
        {
            var basket = await basketRepository.GetCustomerBasket(baskedId);
            return Ok(basket ?? new CustomerBasket() { Id =baskedId });
        }
        [HttpPost]
        public async Task<ActionResult<CustomerBasket>> UpdateBasket(CustomerBasketDto basket)
        {
            var mappedBasket = mapper.Map<CustomerBasketDto, CustomerBasket>(basket);
            var CustomerBasket = await basketRepository.UpdateCustomerBasket(mappedBasket);
            return Ok(CustomerBasket);
        }
        [HttpDelete]
        public async Task<ActionResult<bool>>DeleteBasket(string baskedId)
        {
            return await basketRepository.DeleteCustomerBasket(baskedId);
        }
    }
}
