using Microsoft.OpenApi.Models;
using RapidPay.Business;
using RapidPay.Interface;

namespace RapidPay.Modules
{
    public  static class SwaggerModule
    {

        public static IServiceCollection RegisterSwaggerModule(this IServiceCollection services)
        {

            services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("X-Auth-Token", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.ApiKey,
                    Name = "X-Auth-Token",
                    Description = "X-Auth-Token",
                    In = ParameterLocation.Header
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type=ReferenceType.SecurityScheme,
                                Id="X-Auth-Token"
                            }
                        },
                         new string[]{}
                    }
                });
            });
            return services;
        }
    }
}
