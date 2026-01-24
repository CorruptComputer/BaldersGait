using System.Text.Json.Serialization;
using BaldersGait.Core.Models.Enums;

namespace BaldersGait.Core.Models.BarberShop;

/// <summary>
///   State model for a barber shop chair.
/// </summary>
[JsonSerializable(typeof(BarberShopChair))]
public sealed record BarberShopChair
{
    /// <summary>
    ///   The chair number for this chair
    /// </summary>
    public ChairNumbers ChairNumber { get; init; } = ChairNumbers.Unknown;

    /// <summary>
    ///   Has this chair been unlocked?
    /// </summary>
    public bool Unlocked { get; set; } = false;

    /// <summary>
    ///   The current length of hair grown in this chair.
    /// </summary>
    public double HairLength { get; set; } = 0;

    /// <summary>
    ///   Gets the scaling factor for hair growth based on the number of scaling factor upgrades.
    /// </summary>
    /// <param name="scalingFactorUpgrades"></param>
    /// <returns></returns>
    public double GetHairGrowthScalingFactor(int scalingFactorUpgrades)
    {
        const double baseScalingFactor = 1;
        double actualScalingFactor = baseScalingFactor + (0.1 * scalingFactorUpgrades);

        if (ChairNumber != ChairNumbers.One)
        {
            actualScalingFactor /= Math.Pow(2, (int)ChairNumber - 1);
        }

        return Math.Round(actualScalingFactor, 3);
    }

    /// <summary>
    ///   Gets the hair growth per tick with the scaling factor applied.
    /// </summary>
    /// <param name="baseHairGrowthPerTick"></param>
    /// <param name="scalingFactorUpgrades"></param>
    /// <returns></returns>
    public double GetHairGrowthWithScalingFactor(double baseHairGrowthPerTick, int scalingFactorUpgrades)
    {
        double hairGrowth = baseHairGrowthPerTick;

        hairGrowth *= GetHairGrowthScalingFactor(scalingFactorUpgrades);

        return Math.Round(hairGrowth, 3);
    }

    /// <summary>
    ///   Gets the maximum hair length based on the number of max hair upgrades.
    /// </summary>
    /// <param name="maxHairUpgrades"></param>
    /// <returns></returns>
    public double GetMaxHairLength(int maxHairUpgrades)
    {
        double maxHair = 10 * (maxHairUpgrades + 1);

        return maxHair;
    }

    /// <summary>
    ///   Determines if the chair is ready to have its hair collected.
    /// </summary>
    /// <param name="maxHairUpgrades"></param>
    /// <returns></returns>
    public bool IsReadyToCollect(int maxHairUpgrades)
    {
        return HairLength >= GetMaxHairLength(maxHairUpgrades);
    }

    /// <summary>
    ///   Determines if the hair production rate is higher than the maximum hair length.
    /// </summary>
    /// <param name="baseHairGrowthPerTick"></param>
    /// <param name="maxHairUpgrades"></param>
    /// <param name="scalingFactorUpgrades"></param>
    /// <returns></returns>
    public bool IsProductionTooHigh(double baseHairGrowthPerTick, int maxHairUpgrades, int scalingFactorUpgrades)
    {
        return GetHairGrowthWithScalingFactor(baseHairGrowthPerTick, scalingFactorUpgrades) > GetMaxHairLength(maxHairUpgrades);
    }

    /// <summary>
    ///   Ticks the chairs state and returns the amount of hair generated, if any.
    /// </summary>
    /// <returns></returns>
    public double Tick(double baseHairPerTick, int maxHairUpgrades, int scalingFactorV1Upgrades, bool clippersPurchased)
    {
        double hairGrowth = GetHairGrowthWithScalingFactor(baseHairPerTick, scalingFactorV1Upgrades);
        double maxHairLength = GetMaxHairLength(maxHairUpgrades);

        // If we are making more per tick than we can hold
        if (hairGrowth > maxHairLength)
        {
            HairLength = maxHairLength;

            if (clippersPurchased)
            {
                return HairLength;
            }

            return 0.0;
        }

        // Its full
        if (HairLength >= maxHairLength)
        {
            if (clippersPurchased)
            {
                HairLength = 0;
                return maxHairLength;
            }
            else
            {
                HairLength = maxHairLength;
            }

            return 0.0;
        }

        // Else we can add it to the seats hair length
        HairLength = Math.Round(HairLength + hairGrowth, 3);
        return 0.0;
    }
}