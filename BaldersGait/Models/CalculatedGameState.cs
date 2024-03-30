using System.Text.Json.Serialization;

namespace BaldersGait.Models;

public class CalculatedGameState
{
    public double BaseHairPerTick { get; private set; }

    public void SetBaseHairPerTick(int hairGrowthUpgrades)
    {
        BaseHairPerTick = Math.Round(0.01 * (hairGrowthUpgrades + 1), 3);
    }

    public bool IsMoneyVisible { get; private set; }

    public void SetIsMoneyVisible(bool isWigShopPurchased)
    {
        IsMoneyVisible = isWigShopPurchased;
    }

    public bool IsStylistsButtonVisible { get; private set; }

    public void SetIsStylistsButtonVisible(bool isStylistsPurchased)
    {
        IsStylistsButtonVisible = isStylistsPurchased;
    }

    public bool IsWigShopButtonVisible { get; private set; }

    public void SetIsWigShopButtonVisible(bool isWigShopPurchased)
    {
        IsWigShopButtonVisible = isWigShopPurchased;
    }

    public bool IsHairGrowthV1UpgradesMaxed { get; private set; }
    public void SetIsHairGrowthV1UpgradesMaxed(int hairGrowthV1Upgrades)
    {
        IsHairGrowthV1UpgradesMaxed = hairGrowthV1Upgrades >= GameStateConsts.HairGrowthV1UpgradesMax;
    }

    public bool IsScalingFactorV1UpgradesMaxed { get; private set; }
    public void SetIsScalingFactorV1UpgradesMaxed(int scalingFactorV1Upgrades)
    {
        IsScalingFactorV1UpgradesMaxed = scalingFactorV1Upgrades >= GameStateConsts.ScalingFactorV1UpgradesMax;
    }

    public bool IsMaxHairV1UpgradesMaxed { get; private set; }
    public void SetIsMaxHairV1UpgradesMaxed(int maxHairV1Upgrades)
    {
        IsMaxHairV1UpgradesMaxed = maxHairV1Upgrades >= GameStateConsts.MaxHairV1UpgradesMax;
    }
}