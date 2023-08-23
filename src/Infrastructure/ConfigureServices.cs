using IntegrationContracts;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using ServiceBusPOC.Application.Common;
using ServiceBusPOC.Application.Common.Services.Data;
using ServiceBusPOC.Application.Common.Services.Identity;
using ServiceBusPOC.Application.TodoItems.EventHandlers;
using ServiceBusPOC.Infrastructure.Data;
using ServiceBusPOC.Infrastructure.Data.Interceptors;
using ServiceBusPOC.Infrastructure.Identity;
using ServiceBusPOC.Infrastructure.MessageBroker;
using System.IdentityModel.Tokens.Jwt;

namespace Microsoft.Extensions.DependencyInjection;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

        services.Configure<MessageBrokerSettings>(
            configuration.GetSection("MessageBroker"));
        services.AddSingleton(sp => sp.GetRequiredService<IOptions<MessageBrokerSettings>>().Value);

        services.AddMassTransit(busConfigurator =>
        {
            busConfigurator.SetKebabCaseEndpointNameFormatter();
            busConfigurator.AddConsumer<WorkorderCreatedEventConsumer>();
            busConfigurator.UsingRabbitMq((context, configurator) =>
            {
                MessageBrokerSettings settings = context.GetRequiredService<MessageBrokerSettings>();
                configurator.Host(new Uri(settings.Host), h =>
                {
                    h.Username(settings.Username);
                    h.Password(settings.Password);
                });
                configurator.ConfigureEndpoints(context);
            });
        });

        services.AddTransient<IEventBus, EventBus>();

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString));

        services.AddScoped<ApplicationDbContextInitialiser>();

        services.AddScoped<IApplicationDbContext>(sp =>
            sp.GetRequiredService<ApplicationDbContext>());

        services.AddScoped<AuditableEntitySaveChangesInterceptor>();

        services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
            .AddRoles<ApplicationRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddClaimsPrincipalFactory<ApplicationUserClaimsPrincipalFactory>();

        services.AddIdentityServer()
            .AddApiAuthorization<ApplicationUser, ApplicationDbContext>(options =>
            {
                options.IdentityResources["openid"].UserClaims.Add("role");
                options.ApiResources.Single().UserClaims.Add("role");
                options.IdentityResources["openid"].UserClaims.Add("permissions");
                options.ApiResources.Single().UserClaims.Add("permissions");
            });

        JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Remove("role");

        services.AddScoped<IIdentityService, IdentityService>();

        return services;
    }
}
