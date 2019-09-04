using Autofac;
using Microsoft.AspNetCore.SignalR;
using Services.Interfaces;
using Services.Realizations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace MVC.MyClones
{
    public static class ServiceRegistrator
    {
        public static void Register(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(ServiceRegistrator).GetTypeInfo().Assembly)
                .Where(t => typeof(Hub).IsAssignableFrom(t))
                .ExternallyOwned();

            builder.RegisterType<FieldService>().As<IFieldService>().SingleInstance();
        }
    }
}
