using Autofac;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using BaldersGait.Core;
using BaldersGait.Core.Services.Interface;
using BaldersGait.UI.ViewModels;
using BaldersGait.UI.Views;
using Serilog;
using Serilog.Events;

namespace BaldersGait.UI;

/// <summary>
///   The Avalonia application for BaldersGait
/// </summary>
public class BaldersGait : Application
{
    private IContainer _container = null!;
    private IEnvironmentService _environmentService = null!;

    /// <inheritdoc />
    public override void Initialize()
    {
        ContainerBuilder builder = new();
        builder.RegisterModule(new BaldersGaitUIModule());
        builder.RegisterModule(new BaldersGaitCoreModule());
        _container = builder.Build();
        _environmentService = _container.Resolve<IEnvironmentService>();
        AvaloniaXamlLoader.Load(this);
    }

    /// <inheritdoc />
    public override void OnFrameworkInitializationCompleted()
    {
        Log.Logger = new LoggerConfiguration()
#if DEBUG
            .MinimumLevel.Verbose()
#else
            .MinimumLevel.Warning()
#endif
            .WriteTo.File(Path.Combine(_environmentService.GetUserdataDirectory(), "BaldersGait.log"), rollingInterval: RollingInterval.Day, retainedFileCountLimit: 7)
            .WriteTo.Console(restrictedToMinimumLevel: LogEventLevel.Debug)
            .CreateLogger();

        // Log.Information("Build: {CurrentBuild}", {{ git hash here, need to figure out how }});
        Log.Information("Environment: {CurrentEnvironment}", _environmentService.GetCurrentEnvironment());
        Log.Information($"Userdata directory set to: {_environmentService.GetUserdataDirectory()}");

        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow
            {
                DataContext = _container.Resolve<MainWindowViewModel>(),
            };
        }

        base.OnFrameworkInitializationCompleted();
    }
}
