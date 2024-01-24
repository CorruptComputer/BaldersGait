using System.Reactive.Concurrency;
using System.Runtime.Serialization;
using System.Text.Json;
using BaldersGait.Models.State;
using BaldersGait.Services.Interface;
using Serilog;

namespace BaldersGait.Services;

public class StateService : IStateService
{
    private GameState GameState { get; set; } = new();

    private const string SaveFileName = "GameState.1";
    private string SaveFilePath => Path.Combine(_environmentService.GetUserdataDirectory(), SaveFileName);

    private const string BackupFileName = "GameState.2";
    private string BackupFilePath => Path.Combine(_environmentService.GetUserdataDirectory(), BackupFileName);

    private readonly IEnvironmentService _environmentService;

    public StateService(IEnvironmentService environmentService, bool autoTickState = true)
    {
        _environmentService = environmentService;

        LoadState();

        if (autoTickState)
        {
            TaskPoolScheduler.Default.SchedulePeriodic(TimeSpan.FromMilliseconds(10), TickState);
        }
    }

    public BarberShopState GetBarberShopState()
    {
        return GameState.BarberShopState;
    }

    public bool LoadState(bool resetState = false)
    {
        if (!resetState)
        {
            if (Path.Exists(SaveFilePath))
            {
                try
                {
                    GameState = JsonSerializer.Deserialize<GameState>(File.ReadAllText(SaveFilePath)) ?? throw new SerializationException();
                    return true;
                }
                catch (Exception e) when (e is JsonException or SerializationException)
                {
                    Log.Error($"Error reading save.");
                }
            }

            if (Path.Exists(BackupFilePath))
            {
                try
                {
                    GameState = JsonSerializer.Deserialize<GameState>(File.ReadAllText(BackupFilePath)) ?? throw new SerializationException();
                    return true;
                }
                catch (Exception e) when (e is JsonException or SerializationException)
                {
                    Log.Error($"Error reading save backup.");
                }
            }
        }

        Log.Information($"Creating new save.");
        GameState = new();

        return false;
    }

    public bool SaveState()
    {
        try
        {
            if (Path.Exists(SaveFilePath))
            {
                File.Move(SaveFilePath, BackupFilePath, overwrite: true);
            }

            File.WriteAllText(SaveFilePath, JsonSerializer.Serialize(GameState));
            return true;
        }
        catch (Exception e) when (e is IOException)
        {
            Log.Error($"Error writing save.");
        }

        return false;
    }

    public void TickState()
    {
        GameState.TickMe();
    }
}