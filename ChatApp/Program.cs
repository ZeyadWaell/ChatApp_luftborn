
using ChatApp.Infrastructure.Injection;
using ChatApp.API.Injection;
using ChatApp.Api.Hubs;
using ChatApp.Routes;

namespace ChatApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var configuration = builder.Configuration;

            builder.Services.AddApplicationServices()
                .AddApiServices()
                .AddInfrastructureServices(configuration);


            builder.Services.AddControllers();
            builder.Services.AddSignalR();
            builder.Services.AddOpenApi();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseAuthentication();  
            app.UseAuthorization();   

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<ChatHub>(ChatHubRoutes.Hub);
            });

            app.Run();
        }
    }
}
