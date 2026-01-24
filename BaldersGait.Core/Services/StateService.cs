using System.Reactive.Concurrency;
using System.Runtime.Serialization;
using System.Text.Json;
using BaldersGait.Core.Models.BarberShop;
using BaldersGait.Core.Models.CalculatedState;
using BaldersGait.Core.Models.SavedState;
using BaldersGait.Core.Services.Interface;
using Serilog;

namespace BaldersGait.Core.Services;

internal class StateService : IStateService
{
    public CalculatedGameState CalculatedState { get; }

    public SavedGameState SavedState { get; private set; }

    private const string SaveFileName = "GameState.1";
    private string SaveFilePath => Path.Combine(_environmentService.GetUserdataDirectory(), SaveFileName);

    private const string BackupFileName = "GameState.2";
    private string BackupFilePath => Path.Combine(_environmentService.GetUserdataDirectory(), BackupFileName);

    private readonly IEnvironmentService _environmentService;

    public StateService(IEnvironmentService environmentService, bool autoTickState = true)
    {
        _environmentService = environmentService;

        SavedState = new();
        CalculatedState = new CalculatedGameState(SavedState);

        LoadState();

        if (autoTickState)
        {
            TaskPoolScheduler.Default.SchedulePeriodic(TimeSpan.FromMilliseconds(10), TickState);
        }
    }

    public bool LoadState(bool resetState = false)
    {
        if (!resetState)
        {
            if (Path.Exists(SaveFilePath))
            {
                try
                {
                    SavedState = JsonSerializer.Deserialize<SavedGameState>(File.ReadAllText(SaveFilePath)) ?? throw new SerializationException();
                    CalculatedState.Update(SavedState);
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
                    SavedState = JsonSerializer.Deserialize<SavedGameState>(File.ReadAllText(BackupFilePath)) ?? throw new SerializationException();
                    CalculatedState.Update(SavedState);
                    return true;
                }
                catch (Exception e) when (e is JsonException or SerializationException)
                {
                    Log.Error($"Error reading save backup.");
                }
            }
        }

        Log.Information($"Creating new save.");
        SavedState = new();

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

            File.WriteAllText(SaveFilePath, JsonSerializer.Serialize(SavedState));
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
        foreach (BarberShopChair chair in SavedState.Chairs)
        {
            if (!chair.Unlocked)
            {
                continue;
            }

            double collected = chair.Tick(CalculatedState.BaseHairPerTick, SavedState.MaxHairV1Upgrades, SavedState.ScalingFactorV1Upgrades, SavedState.ClippersPurchased);

            SavedState.HairCollected = Math.Round(SavedState.HairCollected + collected, 3);
        }

        CalculatedState.Update(SavedState);
    }
}