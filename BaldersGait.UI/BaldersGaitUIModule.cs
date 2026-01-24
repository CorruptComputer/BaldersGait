using Autofac;

namespace BaldersGait.UI;

/// <summary>
///   UI module for the game, things get registered with Autofac here.
/// </summary>
public class BaldersGaitUIModule : Module
{
    /// <inheritdoc />
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterAssemblyTypes(ThisAssembly)
            .Where(t => t.Name.EndsWith("ViewModel"));
    }
}