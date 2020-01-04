using Autofac;
using Autofac.Configuration;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Reflection;

namespace MVC.MyClones
{
    public static class ServiceRegistrator
    {
        public static void Register(ContainerBuilder builder)
        {
            // Add the configuration to the ConfigurationBuilder.
            var config = new ConfigurationBuilder();
            // config.AddJsonFile comes from Microsoft.Extensions.Configuration.Json
            // config.AddXmlFile comes from Microsoft.Extensions.Configuration.Xml
            config.AddJsonFile("autofac.json");

            // Register the ConfigurationModule with Autofac.
            var b = config.Build();
            var module = new ConfigurationModule(b);

            builder.RegisterModule(module);

            builder.RegisterAssemblyTypes(typeof(ServiceRegistrator).GetTypeInfo().Assembly)
                .Where(t => typeof(Hub).IsAssignableFrom(t))
                .ExternallyOwned();
        }
    }
}
