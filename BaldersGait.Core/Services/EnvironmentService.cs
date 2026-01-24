using System.Runtime.InteropServices;
using BaldersGait.Core.Models.Enums;
using BaldersGait.Core.Services.Interface;

namespace BaldersGait.Core.Services;

internal class EnvironmentService : IEnvironmentService
{
    private readonly CurrentEnvironment _currentEnvironment;
    private readonly string _userdataDirectory;

    public EnvironmentService()
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            _currentEnvironment = CurrentEnvironment.Windows;
        }
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
        {
            _currentEnvironment = CurrentEnvironment.MacOS;
        }
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            _currentEnvironment = CurrentEnvironment.Linux;
        }

        _userdataDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "BaldersGait");

        if (!Directory.Exists(_userdataDirectory))
        {
            Directory.CreateDirectory(_userdataDirectory);
        }
    }

    public CurrentEnvironment GetCurrentEnvironment()
    {
        return _currentEnvironment;
    }

    public string GetUserdataDirectory()
    {
        return _userdataDirectory;
    }
}