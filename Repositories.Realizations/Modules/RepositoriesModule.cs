using Autofac;
using Repositories.Interfaces.Fields;
using Repositories.Realizations.Fields;

namespace Repositories.Implementations.Modules
{
    public class RepositoriesModule : Module
    {
        public RepositoriesModule(ContainerBuilder builder)
        {
            _builder = builder;
        }

        private ContainerBuilder _builder;

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<FieldsRepository>().As<IFieldsRepository>().SingleInstance();
        }

        public void LoadRepositories()
        {
            Load(_builder);
        }
    }
}
