using BaldersGait.Models.Enums;
using BaldersGait.Models.Stylists;

namespace BaldersGait.Models.BarberShop;

[Serializable]
public class BarberShopChair
{
    public ChairNumbers ChairNumber { get; init; } = ChairNumbers.Unknown;
    public bool Unlocked { get; set; } = false;
    public double HairLength { get; set; } = 0;

    public Stylist? StylistAssigned { get; set; } = null;

    public double GetHairGrowthScalingFactor(int scalingFactorUpgrades)
    {
        const double baseScalingFactor = 1;
        double actualScalingFactor = baseScalingFactor + (0.1 * scalingFactorUpgrades);
        
        if (ChairNumber != ChairNumbers.One)
        {
            // Apply Chair Penalty
            if (StylistAssigned == null)
            {
                actualScalingFactor /= Math.Pow(2, (int)ChairNumber - 1);
            }
            else
            {
                actualScalingFactor *= StylistAssigned.MultiplicativeScalingFactorBonus;
            }
        }
        
        return Math.Round(actualScalingFactor, 3);
    }
    
    public double GetHairGrowthWithScalingFactor(double baseHairGrowthPerTick, int scalingFactorUpgrades)
    {
        double hairGrowth = baseHairGrowthPerTick;

        if (StylistAssigned != null)
        {
            hairGrowth += (hairGrowth * StylistAssigned.AdditiveHairGrowthSpeedBonus);
            hairGrowth *= GetHairGrowthScalingFactor(scalingFactorUpgrades);
        }
        else
        {
            hairGrowth *= GetHairGrowthScalingFactor(scalingFactorUpgrades);
        }
        
        return Math.Round(hairGrowth, 3);
    }
    
    public double GetMaxHairLength(int maxHairUpgrades)
    {
        double maxHair = 10 * (maxHairUpgrades + 1);

        if (StylistAssigned != null)
        {
            maxHair *= StylistAssigned.MultiplicativeMaxHairBonus;
        }
        
        return maxHair;
    }
    
    public bool IsReadyToCollect(int maxHairUpgrades)
    {
        return HairLength >= GetMaxHairLength(maxHairUpgrades);
    }

    public bool IsProductionTooHigh(double baseHairGrowthPerTick, int maxHairUpgrades, int scalingFactorUpgrades)
    {
        return GetHairGrowthWithScalingFactor(baseHairGrowthPerTick, scalingFactorUpgrades) > GetMaxHairLength(maxHairUpgrades);
    }
}