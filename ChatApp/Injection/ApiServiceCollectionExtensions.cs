using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace ChatApp.API.Injection
{
    public static class ApiServiceCollectionExtensions
    {
        public static IServiceCollection AddApiServices(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ChatApp API", Version = "v1" });
            });
            return services;
        }
    }
}
