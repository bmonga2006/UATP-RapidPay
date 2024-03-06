using Microsoft.AspNetCore.Mvc;
using RapidPay.Business;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RapidPay.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
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
