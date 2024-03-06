using Microsoft.Extensions.DependencyInjection;
using RapidPay.Business;
using RapidPay.Interface;

namespace RapidPay.Modules
{
    public static class PaymentFeeModule
    {
        public static IServiceCollection RegisterPaymentFeeModule(this IServiceCollection services, IConfiguration configuration)
        {

            
            services.AddSingleton<PaymentFeeManager>(PaymentFeeManager.GetInstance(configuration));            
            return services;
        }
    }
}
