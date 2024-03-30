using System.Reactive.Concurrency;
using System.Runtime.Serialization;
using System.Text.Json;
using BaldersGait.Models;
using BaldersGait.Services.Interface;
using Serilog;

namespace BaldersGait.Services;

public class StateService : IStateService
{
    public CalculatedGameState CalculatedState { get; private set; } = new();

    public SavedGameState SavedState { get; private set; } = new();

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

    public bool LoadState(bool resetState = false)
    {
        if (!resetState)
        {
            if (Path.Exists(SaveFilePath))
            {
                try
                {
                    SavedState = JsonSerializer.Deserialize<SavedGameState>(File.ReadAllText(SaveFilePath)) ?? throw new SerializationException();
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
        CalculatedState.SetBaseHairPerTick(SavedState.HairGrowthV1Upgrades);
        CalculatedState.SetIsStylistsButtonVisible(SavedState.StylistsPurchased);
        CalculatedState.SetIsMoneyVisible(SavedState.CompanyPurchased);
        CalculatedState.SetIsWigShopButtonVisible(SavedState.CompanyPurchased);
        CalculatedState.SetIsHairGrowthV1UpgradesMaxed(SavedState.HairGrowthV1Upgrades);
        CalculatedState.SetIsScalingFactorV1UpgradesMaxed(SavedState.ScalingFactorV1Upgrades);
        CalculatedState.SetIsMaxHairV1UpgradesMaxed(SavedState.MaxHairV1Upgrades);

        Parallel.ForEach(SavedState.Chairs.Where(x => x.Unlocked), seat =>
        {
            double hairGrowth = seat.GetHairGrowthWithScalingFactor(CalculatedState.BaseHairPerTick, SavedState.ScalingFactorV1Upgrades);
            double maxHairLength = seat.GetMaxHairLength(SavedState.MaxHairV1Upgrades);

            // If we are making more per tick than we can hold
            if (hairGrowth > maxHairLength)
            {
                seat.HairLength = maxHairLength;

                if (SavedState.ClippersPurchased)
                {
                    SavedState.HairCollected = Math.Round(SavedState.HairCollected + seat.HairLength, 3);
                }

                return;
            }

            // If we are full
            if (seat.HairLength >= maxHairLength)
            {
                if (SavedState.ClippersPurchased)
                {
                    SavedState.HairCollected = Math.Round(SavedState.HairCollected + maxHairLength, 3);
                    seat.HairLength = 0;
                }
                else
                {
                    seat.HairLength = maxHairLength;
                }

                return;
            }

            // Else we can add it to the seats hair length
            seat.HairLength = Math.Round(seat.HairLength + hairGrowth, 3);
        });
    }
}