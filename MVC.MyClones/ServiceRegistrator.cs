using Autofac;
using Microsoft.AspNetCore.SignalR;
using Repositories.Implementations.Modules;
using Services.Implementations.Modules;
using System.Linq;
using System.Reflection;

namespace MVC.MyClones
{
    public static class ServiceRegistrator
    {
        public static void Register(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(ServiceRegistrator).GetTypeInfo().Assembly)
                .Where(t => typeof(Hub).IsAssignableFrom(t))
                .ExternallyOwned();

            var servicesModule = new ServicesModule(builder);
            servicesModule.LoadServices();

            var repositoriesModule = new RepositoriesModule(builder);
            repositoriesModule.LoadRepositories();
        }
    }
}
