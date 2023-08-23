using Autofac;
using Autofac.Integration.Web;
using MassTransit;
using MedusaReplica.IntegrationHandlers;
using System;
using System.Configuration;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;

namespace MedusaReplica
{
    public class Global : HttpApplication, IContainerProviderAccessor
    {
        // Provider that holds the application container.
        static IContainerProvider _containerProvider;

        // Instance property that will be used by Autofac HttpModules
        // to resolve and inject dependencies.
        public IContainerProvider ContainerProvider
        {
            get { return _containerProvider; }
        }

        protected void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            // Build up your application container and register your dependencies.
            var builder = new ContainerBuilder();

            builder.AddMassTransit(x =>
            {
                x.AddConsumer<TodoItemCreatedIntegrationEventConsumer>();
                x.AddBus(context => Bus.Factory.CreateUsingRabbitMq(cfg =>
                {
                    cfg.Host(new Uri("amqp://localhost:5672"), h =>
                    {
                        h.Username("guest");
                        h.Password("guest");
                    });
                    cfg.ConfigureEndpoints(context);
                }));
            });

            // ... continue registering dependencies...

            // Once you're done registering things, set the container
            // provider up with your registrations.
            _containerProvider = new ContainerProvider(builder.Build());

            var bc = _containerProvider.ApplicationContainer.Resolve<IBusControl>();
            bc.Start();
        }
    }
}