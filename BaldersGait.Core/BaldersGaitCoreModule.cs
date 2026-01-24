using Autofac;
using BaldersGait.Core.Services.Interface;
using BaldersGait.Core.Services;

namespace BaldersGait.Core;

/// <summary>
///   Main module for the game, things get registered with Autofac here.
/// </summary>
public class BaldersGaitCoreModule : Module
{
    /// <inheritdoc />
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
