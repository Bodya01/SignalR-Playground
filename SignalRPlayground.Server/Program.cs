using Microsoft.AspNetCore.Http.Connections;
using SignalRPlayground.Server.Hubs;
using SignalRPlayground.Server.Services.Implementations;
using SignalRPlayground.Server.Services.Interfaces;
using System.Text.Json.Serialization;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddSignalR();
        builder.Services.AddScoped<INotificationService, NotificationService>();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseAuthorization();

#pragma warning disable ASP0014 // Suggest using top level route registrations
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapHub<PlaygroundHub>("/playground-hub", options =>
            {
                options.Transports = HttpTransportType.WebSockets;
            });
        });
#pragma warning restore ASP0014 // Suggest using top level route registrations

        app.Run();
    }
}