using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talapat.BLL.Interfaces;
using Talapat.DAL.Entities.RedisEntities;

namespace Talapat.BLL.Repositories
{
    public class BasketRpository : IBasketRepository
    {        
        private readonly IDatabase _database;
        public BasketRpository(IConnectionMultiplexer Redis)
        {
            _database = Redis.GetDatabase();
        }
        public async Task<bool> DeleteCustomerBasket(string basketId)
        {
            return await _database.KeyDeleteAsync(basketId);
        }

        public async Task<CustomerBasket> GetCustomerBasket(string basketId)
        {
            var basket = await _database.StringGetAsync(basketId);
            return (basket.IsNullOrEmpty) ? null : JsonSerializer.Deserialize<CustomerBasket>(basket);
        }

        public async Task<CustomerBasket> UpdateCustomerBasket(CustomerBasket basket)
        {
            var Created =await _database.StringSetAsync(basket.Id, JsonSerializer.Serialize(basket), TimeSpan.FromDays(30));
            if (!Created) return null;
            return await GetCustomerBasket(basket.Id);
        }
    }
}
