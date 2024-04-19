using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talapat.BLL.Interfaces;
using Talapat.BLL.Specifications.OrderSpecification;
using Talapat.DAL.Entities;
using Talapat.DAL.Entities.Order_Aggregate;

namespace Talapat.BLL.Services
{
    public class OrderService : IOrderService
    {
        private readonly IBasketRepository basketRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IPaymentService paymentService;

        //private readonly IGenericRepository<Product> productRepo;
        //private readonly IGenericRepository<DeliveryMethod> deliveryMethodRepo;
        //private readonly IGenericRepository<Order> orderRepo;

        public OrderService(IBasketRepository basketRepository
            //,IGenericRepository<Product> ProductRepo,IGenericRepository<DeliveryMethod> DeliveryMethodRepo
            //,IGenericRepository<Order> OrderRepo
            ,IUnitOfWork unitOfWork,IPaymentService paymentService)
        {
            this.basketRepository = basketRepository;
            this.unitOfWork = unitOfWork;
            this.paymentService = paymentService;
            //productRepo = ProductRepo;
            //deliveryMethodRepo = DeliveryMethodRepo;
            //orderRepo = OrderRepo;
        }
        public async Task<Order> CreateOrderAsync(string BuyerEmail, string BasketId, int deliveryMethodId, Address ShipToAddress)
        {
            var Basket = await basketRepository.GetCustomerBasket(BasketId);
            var OrderItems = new List<OrderItem>();
            foreach (var item in Basket.Items)
            {
                var Product = await unitOfWork.Repoistory<Product>().GetById(item.Id);
                var ProductItemOrder = new ProductItemOrder(Product.Id, Product.Name, Product.PictureUrl);
                var OrderItem = new OrderItem(ProductItemOrder, Product.Price, item.Quantity);
                OrderItems.Add(OrderItem);
            }
            var DeliveryMethod =await unitOfWork.Repoistory<DeliveryMethod>().GetById(deliveryMethodId);
            var SubTotal = OrderItems.Sum(i => i.Price * i.Quantity);
            //check If order is exist or not
            var spec = new OrderWithItemsByPaymentIntentSpecification(Basket.PaymentIntentId);
            var existingOrder = await unitOfWork.Repoistory<Order>().GetByIdWithSpec(spec);
            if(existingOrder != null)
            {
                unitOfWork.Repoistory<Order>().Delete(existingOrder);
                await paymentService.CreateOrUpdatePaymentIntent(BasketId);
            }
            var Order = new Order(BuyerEmail, ShipToAddress, DeliveryMethod, OrderItems,SubTotal,Basket.PaymentIntentId);
            await unitOfWork.Repoistory<Order>().Add(Order);
            //TODO Save To Database
            int result = await unitOfWork.Complete();
            if (result <= 0)
                return null;

            return Order;
        }

        public  async Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodAsync()
        {
            return await unitOfWork.Repoistory<DeliveryMethod>().GetAllAsync();
        }

        public async Task<Order> GetOrderByIdForUser(int OrderId, string BuyerEmail)
        {
            var spec = new OrderWithItemsAndDeliveryMethodSpecification(OrderId, BuyerEmail);
            var order = await unitOfWork.Repoistory<Order>().GetByIdWithSpec(spec);
            return order;
        }

        public async Task<IReadOnlyList<Order>> GetOrderForUserAsync(string BuyerEmail)
        {
            var spec = new OrderWithItemsAndDeliveryMethodSpecification(BuyerEmail);
            var orders = await unitOfWork.Repoistory<Order>().GetAllWithSpecAsync(spec);
            return orders;            
        }
    }
}
