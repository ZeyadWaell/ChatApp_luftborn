using ChatApp.Application.Strategies.Bot;
using ChatApp.Core.Interfaces.Main;
using Microsoft.Extensions.DependencyInjection;

namespace ChatApp.Api.ModuleInfrastructureDependencies
{
    public static class ApplicationServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // Register MediatR, services, and AutoMapper if needed.
            services.AddMediatR(typeof(CreateChatMessageCommand).Assembly);
            services.AddTransient<IChatMessageService, ChatMessageService>();
            services.AddTransient<IBotStrategyFactory, BotStrategyFactory>();
            return services;
        }
    }

}
