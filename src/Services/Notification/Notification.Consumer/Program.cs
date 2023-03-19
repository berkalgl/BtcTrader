using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Notification.Consumer.Consumers;
using Notification.Consumer.Services;
using Notification.Consumer.Strategies;
using Notification.Domain.AggregatesModel;
using Notification.Domain.Services;
using Notification.Infrastructure;
using Notification.Infrastructure.Repositories;

var host = Host
    .CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        services.AddDbContext<NotificationDbContext>(opt => opt.UseInMemoryDatabase(databaseName: "NotificationDb"));
        services.AddMassTransit(x =>
        {
            x.AddConsumer<NotificationCreatedEventConsumer>();

            x.UsingRabbitMq((ctx, cfg) =>
            {
                cfg.Host("rabbitmq", "/", host =>
                {
                    host.Username("guest");
                    host.Password("guest");
                });

                cfg.ReceiveEndpoint("NotificationCreatedEvent", e =>
                {
                    e.ConfigureConsumer<NotificationCreatedEventConsumer>(ctx);
                });
            });
            Console.WriteLine("RabbitMQ Configuration done!");
        });
        services.AddScoped<INotificationProviderFactory, NotificationProviderFactory>();
        services.AddScoped<INotificationService, NotificationService>();
        services.AddScoped<INotificationRepository, NotificationRepository>();
    });

await host.RunConsoleAsync();
