using BaldersGait.Models.Enums;

namespace BaldersGait.Services.Interface;

/// <summary>
///   The environment service should handle detecting the environment, setting up environment specific configurations, and doing environment specific tasks.
/// </summary>
public interface IEnvironmentService
{
    /// <summary>
    ///   Gets the current environment that the game is running on.
    /// </summary>
    /// <returns>The current environment that the game is running on.</returns>
    public CurrentEnvironment GetCurrentEnvironment();

    /// <summary>
    ///   Gets the userdata directory for the current environment.
    /// </summary>
    /// <returns>The userdata directory for the current environment.</returns>
    public string GetUserdataDirectory();
}