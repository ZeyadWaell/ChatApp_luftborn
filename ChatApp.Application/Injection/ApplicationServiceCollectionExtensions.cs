using ChatApp.Application.BotStrategies;
using ChatApp.Application.CQRS.Commands.Auth;
using ChatApp.Application.Services.CQRSS.inteface;
using ChatApp.Application.Services.CQRSS;
using ChatApp.Application.Strategies.Bot;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using ChatApp.Application.Services;

public static class ApplicationServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(LoginUserCommandHandler).Assembly));

        services.AddTransient<ITokenService, TokenService>();
        services.AddTransient<IBotStrategyFactory, BotStrategyFactory>();
        services.AddTransient<IAuthService, AuthService>();

        return services;
    }
}
