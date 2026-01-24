using BaldersGait.Core.Consts;
using BaldersGait.Core.Models.SavedState;

namespace BaldersGait.Core.Models.CalculatedState;

/// <summary>
///   Game state thats calculated at runtime, nothing in here is saved
/// </summary>
public sealed class CalculatedGameState
{
    /// <summary>
    ///   ctor
    /// </summary>
    /// <param name="savedGameState"></param>
    public CalculatedGameState(SavedGameState savedGameState)
    {
        Update(savedGameState);
    }

    /// <summary>
    ///   The base hair per tick before chair scaling modifiers
    /// </summary>
    public double BaseHairPerTick { get; private set; }

    /// <summary>
    ///   Should money be visible in the UI?
    /// </summary>
    public bool IsMoneyVisible { get; private set; }

    /// <summary>
    ///   Should the wig shop button be visible in the UI?
    /// </summary>
    public bool IsWigShopButtonVisible { get; private set; }

    /// <summary>
    ///   Are the hair growth V1 upgrades maxed out?
    /// </summary>
    public bool IsHairGrowthV1UpgradesMaxed { get; private set; }

    /// <summary>
    ///   Are the scaling factor V1 upgrades maxed out?
    /// </summary>
    public bool IsScalingFactorV1UpgradesMaxed { get; private set; }

    /// <summary>
    ///   Are the max hair V1 upgrades maxed out?
    /// </summary>
    public bool IsMaxHairV1UpgradesMaxed { get; private set; }

    /// <summary>
    ///   Updates the calculated state with the current saved game state
    /// </summary>
    /// <param name="savedGameState"></param>
    public void Update(SavedGameState savedGameState)
    {
        BaseHairPerTick = Math.Round(0.01 * (savedGameState.HairGrowthV1Upgrades + 1), 3); ;
        IsMoneyVisible = savedGameState.CompanyPurchased;
        IsWigShopButtonVisible = savedGameState.CompanyPurchased;
        IsHairGrowthV1UpgradesMaxed = savedGameState.HairGrowthV1Upgrades >= GameStateConsts.HairGrowthV1UpgradesMax;
        IsScalingFactorV1UpgradesMaxed = savedGameState.ScalingFactorV1Upgrades >= GameStateConsts.ScalingFactorV1UpgradesMax;
        IsMaxHairV1UpgradesMaxed = savedGameState.MaxHairV1Upgrades >= GameStateConsts.MaxHairV1UpgradesMax;
    }
}