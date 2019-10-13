using Autofac;
using Microsoft.AspNetCore.SignalR;
using Repositories.Interfaces.Fields;
using Repositories.Realizations.Fields;
using Services.Interfaces;
using Services.Interfaces.Fields;
using Services.Realizations;
using Services.Realizations.Fields;
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
            builder.RegisterType<FieldsStageService>().As<IFieldsStageService>().SingleInstance();

            builder.RegisterType<FieldsRepository>().As<IFieldsRepository>().SingleInstance();
        }
    }
}
