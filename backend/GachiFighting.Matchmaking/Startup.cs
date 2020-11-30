
using GachiFighting.Matchmaking.Game;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace GachiFighting.Matchmaking
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSignalR();
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder
                        .WithOrigins("http://localhost:3000")
                        .WithHeaders(new string[] { "X-Requested-With", "x-signalr-user-agent" })
                        .AllowCredentials();
                });
            });

            services.AddSingleton(new PlayerRegistry());
            services.AddSingleton(new GameRegistry());
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();

            app.UseCors();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<GameHub>("/game");
            });
        }
    }
}
