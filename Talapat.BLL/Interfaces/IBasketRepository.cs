using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talapat.DAL.Entities.RedisEntities;

namespace Talapat.BLL.Interfaces
{
    public interface IBasketRepository
    {
        public Task<CustomerBasket> GetCustomerBasket(string basketId);
        public Task<CustomerBasket> UpdateCustomerBasket(CustomerBasket basket);
        public Task<bool> DeleteCustomerBasket(string basketId);
    }
}
