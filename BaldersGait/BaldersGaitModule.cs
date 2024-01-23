using Autofac;
using BaldersGait.Services;
using BaldersGait.Services.Interface;

namespace BaldersGait;

public class BaldersGaitModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        //builder.RegisterType<HttpClient>().SingleInstance();

        // Services
        builder.RegisterType<EnvironmentService>().As<IEnvironmentService>().SingleInstance();
        builder.RegisterType<StateService>().As<IStateService>().SingleInstance();

        //builder.RegisterType<PreferencesService>().As<IPreferencesService>().SingleInstance();

        // View Models
        builder.RegisterAssemblyTypes(ThisAssembly)
            .Where(t => t.Name.EndsWith("ViewModel"));
    }
}