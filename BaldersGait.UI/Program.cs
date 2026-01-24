using Avalonia;
using Avalonia.ReactiveUI;

namespace BaldersGait.UI;

/// <summary>
///   Main entry point
/// </summary>
public static class Program
{
    /// <summary>
    ///   Main entry point
    /// </summary>
    /// <remarks>
    ///   Initialization code. Don't use any Avalonia, third-party APIs or any
    ///   SynchronizationContext-reliant code before AppMain is called: things aren't initialized
    ///   yet and stuff might break.
    /// </remarks>
    /// <param name="args"></param>
    [STAThread]
    public static void Main(string[] args)
        => BuildAvaloniaApp().StartWithClassicDesktopLifetime(args);

    // Avalonia configuration, don't remove; also used by visual designer.
    private static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<BaldersGait>()
            .UsePlatformDetect()
            .WithInterFont()
            .LogToTrace()
            .UseReactiveUI()
            .WithInterFont();
}