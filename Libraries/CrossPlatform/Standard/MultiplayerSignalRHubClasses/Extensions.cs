using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
namespace MultiplayerSignalRHubClasses
{
    public static class Extensions
    {
        public static void AddMultiplayerSignalRServices(this IServiceCollection services)
        {
            services.AddSignalR(options =>
            {
                options.MaximumReceiveMessageSize = 72428800;
            }); //don't use core. core is only bare necessities.
            services.AddCors(options =>
            {
                options.AddPolicy("AllowOrigin", builder =>
                {
                    builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
                });
            });
        }
        public static void AddMultiplayerSignalRServices(this IApplicationBuilder app)
        {
            app.UseCors("AllowOrigin");
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<MultiplayerConnectionHub>("/hubs/gamepackage/messages", options =>
                {
                    options.TransportMaxBufferSize = 72428800;
                    options.ApplicationMaxBufferSize = 72428800;
                });
            });
        }
    }
}
