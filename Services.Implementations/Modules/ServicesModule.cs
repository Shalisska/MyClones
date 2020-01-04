using Autofac;
using Services.Contracts.Fields;
using Services.Implementations.Fields;

namespace Services.Implementations.Modules
{
    public class ServicesModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<FieldService>().As<IFieldService>().SingleInstance();
            builder.RegisterType<FieldsStageService>().As<IFieldsStageService>().SingleInstance();
        }
    }
}