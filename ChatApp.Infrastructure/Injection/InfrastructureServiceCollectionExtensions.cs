using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using ChatApp.Infrastructure.Data;
using ChatApp.Core.Interfaces;
using ChatApp.Infrastructure.Repositories;


namespace ChatApp.Infrastructure.Injection
{
    public static class InfrastructureServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationIdentityDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("IdentityConnection")));
            services.AddDbContext<ChatDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("ChatConnection")));

       //     services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IChatMessageRepository, ChatMessageRepository>();
            services.AddScoped<IChatRoomRepository, ChatRoomRepository>();
            services.AddScoped<IChatRoomMemberRepository, ChatRoomMemberRepository>();

            // Register external bot strategies
            services.AddTransient<IBotStrategy, GeminiBotStrategy>();
            services.AddTransient<IBotStrategy, ChatGPTBotStrategy>();

            return services;
        }
    }
}
