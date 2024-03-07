using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RapidPay.Managers;

namespace RapidPay.Controllers
{
    [Route("api/v1/[controller]")]   
    [ApiController]
    [Authorize]
    public class PaymentFeeController : ControllerBase
    {
        private readonly PaymentFeeManager PaymentFeeManager;

        public PaymentFeeController(PaymentFeeManager paymentFeeManager)
        {
            PaymentFeeManager = paymentFeeManager;
        }

        [HttpGet]
        public string GetCurrentFee()
        {
            return PaymentFeeManager.GetCurrentFeeAndNextUpdateSpan();
        }

        
    }
}
