using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using Talapat.Api.Errors;
using Talapat.BLL.Interfaces;
using Talapat.DAL.Entities.RedisEntities;

namespace Talapat.Api.Controllers
{
    public class PaymentsController : BaseApiController
    {
        private readonly IPaymentService paymentService;
        private readonly ILogger<PaymentsController> logger;
        private const string _weSecret= "whsec_2f827fe62ec4151708daa8f48cf99486eea6aec8bdd4d2bd12dcc3e25aad26a0";
        public PaymentsController(IPaymentService paymentService,ILogger<PaymentsController> logger)
        {
            this.paymentService = paymentService;
            this.logger = logger;
        }
        [Authorize]
        [HttpPost("{basketId}")]
        public async Task<ActionResult<CustomerBasket>> CreateOrUpdatePaymentIntent(string basketId)
        {
            var basket = await paymentService.CreateOrUpdatePaymentIntent(basketId);
            if (basketId == null)
                BadRequest(new ApiResponse(400, "Problem With Your Basket"));
            return Ok(basket);
        }
        [HttpPost("webhook")]
        public async Task<ActionResult> StripeWebhook()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
            try
            {
                var stripeEvent = EventUtility.ConstructEvent(json,
                    Request.Headers["Stripe-Signature"], _weSecret);
                PaymentIntent intent;
                Talapat.DAL.Entities.Order_Aggregate.Order order;
                switch(stripeEvent.Type)
                {
                    case Events.PaymentIntentSucceeded:
                        logger.LogInformation("Payment Succedded");
                        intent = (PaymentIntent)stripeEvent.Data.Object;
                        order = await paymentService.UpdateOrderPaymentSucceded(intent.Id);
                        break;
                    case Events.PaymentIntentPaymentFailed:
                        intent = (PaymentIntent)stripeEvent.Data.Object;
                        order = await paymentService.UpdateOrderPaymentFailed(intent.Id);
                        logger.LogInformation("Payment Failed",order.Id);
                        logger.LogInformation("Payment Failed",intent.Id);
                        break;
                }
                return new EmptyResult();
            }
            catch (StripeException e)
            {
                return BadRequest();
            }

        }

    }
}
