namespace ChatApp.Api.Injection
{
    public static class ApiServiceCollectionExtensions
    {
        public static IServiceCollection AddApiServices(this IServiceCollection services)
        {
            // Register Swagger, API versioning, etc.
            services.AddSwaggerGen();
            // Additional API-specific services.
            return services;
        }
    }

}
