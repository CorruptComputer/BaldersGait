using System.Text.Json.Serialization;

namespace BaldersGait.Models.State.BarberShop;

[Serializable]
public class BarberShopChair
{
    public ChairNumbers ChairNumber { get; set; } = ChairNumbers.Unknown;
    public bool Unlocked { get; set; } = false;
    public double HairLength { get; set; } = 0;

    [JsonIgnore]
    public double HairGrowthScalingFactor => Math.Round(ChairNumber switch
    {
        ChairNumbers.One => 1,
        _ => (double)ChairNumber / Math.Pow(2, (int)ChairNumber)
    }, 3);

    [JsonIgnore]
    public bool ReadyToCollect => HairLength >= GetMaxHairLength();

    public double GetHairGrowthWithScalingFactor(double baseHairGrowthPerTick)
    {
        return Math.Round(baseHairGrowthPerTick * HairGrowthScalingFactor, 3);
    }

    public bool IsProductionTooHigh(double baseHairGrowthPerTick)
    {
        return GetHairGrowthWithScalingFactor(baseHairGrowthPerTick) > GetMaxHairLength();
    }

    public double GetMaxHairLength()
    {
        // TODO: Add upgrades for this
        return 10;
    }
}