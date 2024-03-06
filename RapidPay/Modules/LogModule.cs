using RapidPay.Interfaces;
using RapidPay.Managers;

namespace RapidPay.Modules
{
    public static class LogModule
    {
        public static void RegisterLoggerModule(this IServiceCollection services)
        {
            services.AddSingleton<ILoggerManager, LoggerManager>();
        }
    }
}
