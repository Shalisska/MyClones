using Autofac;
using Repositories.Contracts.Fields;
using Repositories.Realizations.Fields;

namespace Repositories.Implementations.Modules
{
    public class RepositoriesModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<FieldsRepository>().As<IFieldsRepository>().SingleInstance();
        }
        //public RepositoriesModule(ContainerBuilder builder)
        //{
        //    _builder = builder;
        //}

        //private ContainerBuilder _builder;

        //protected override void Load(ContainerBuilder builder)
        //{
        //    builder.RegisterType<FieldsRepository>().As<IFieldsRepository>().SingleInstance();
        //}

        //public void LoadRepositories()
        //{
        //    Load(_builder);
        //}
    }
}
