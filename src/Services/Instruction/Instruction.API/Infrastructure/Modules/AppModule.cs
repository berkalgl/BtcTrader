using FluentValidation;
using FluentValidation.AspNetCore;
using Instruction.API.Application.Queries;
using Instruction.Domain.AggregatesModel;
using Instruction.Domain.Services;
using Instruction.Infrastructure.Repositories;
using MassTransit;
using MessagesAndEvents.Events.V1;

namespace Instruction.API.Infrastructure.Modules
{
    public static class AppModule
    {
        public static void Register(this IServiceCollection services)
        {
            services.RegisterFluentValidation();
            services.RegisterMediatR();
            services.RegisterServices();
            services.MassTransitConfiguration();
        }
        private static void RegisterMediatR(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<Program>());
        }
        private static void RegisterFluentValidation(this IServiceCollection services)
        {
            services.AddFluentValidationAutoValidation();
            services.AddFluentValidationClientsideAdapters();
            services.AddValidatorsFromAssemblyContaining(typeof(Program));
        }
        private static void RegisterServices(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IInstructionRepository, InstructionRepository>();
            services.AddScoped<IInstructionService, InstructionService>();
            services.AddScoped<IInstructionQueries, InstructionQueries>();
        }
        private static void MassTransitConfiguration(this IServiceCollection services)
        {
            services.AddMassTransit(configurator =>
            {
                configurator.UsingRabbitMq((context, config) =>
                {
                    config.Host("rabbitmq", "/", host =>
                    {
                        host.Username("guest");
                        host.Password("guest");
                    });

                    config.ConfigureEndpoints(context);
                });
            });
        }
    }
}
