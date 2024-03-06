using Microsoft.EntityFrameworkCore;
using RapidPay.Data;

namespace RapidPay.Modules
{
    public static class DBContextModule
    {
        public static IServiceCollection RegisterDBContextModule(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddDbContext<ApplicationDBContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });
            return services;
        }
    }
}
