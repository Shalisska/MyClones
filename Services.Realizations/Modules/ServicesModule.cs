﻿using Autofac;
using Services.Contracts;
using Services.Contracts.Fields;
using Services.Implementations.Fields;

namespace Services.Implementations.Modules
{
    public class ServicesModule : Module
    {
        public ServicesModule(ContainerBuilder builder)
        {
            _builder = builder;
        }

        private ContainerBuilder _builder;

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<FieldService>().As<IFieldService>().SingleInstance();
            builder.RegisterType<FieldsStageService>().As<IFieldsStageService>().SingleInstance();
        }

        public void LoadServices()
        {
            Load(_builder);
        }
    }
}
