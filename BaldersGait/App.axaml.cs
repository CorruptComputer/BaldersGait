using System.IO;
using Autofac;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using BaldersGait.Services.Interface;
using BaldersGait.ViewModels;
using BaldersGait.Views;
using Serilog;
using Serilog.Events;

namespace BaldersGait;

public class App : Application
{
    private IContainer _container = null!;
    private IEnvironmentService _environmentService = null!;

    public override void Initialize()
    {
        ContainerBuilder builder = new();
        builder.RegisterModule(new BaldersGaitModule());
        _container = builder.Build();
        _environmentService = _container.Resolve<IEnvironmentService>();
        AvaloniaXamlLoader.Load(this);
    }

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
