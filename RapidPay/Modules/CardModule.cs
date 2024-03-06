using RapidPay.Business;
using RapidPay.Interface;

namespace RapidPay.Modules
{
    public static class CardModule
    {
        public static IServiceCollection RegisterCardModule(this IServiceCollection services)
        {
            
            services.AddScoped<ICardManager, CardManager>();
            return services;
        }
    }
}
