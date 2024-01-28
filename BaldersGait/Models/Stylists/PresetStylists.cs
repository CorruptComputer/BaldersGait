using BaldersGait.Models.Enums;

namespace BaldersGait.Models.Stylists;

public static class PresetStylists
{
    private static readonly List<Stylist> Presets = [
        new()
        {
            Preset = true,
            Name = "Copyright",
            BaseCostPerHour = 100,
            
            HairGrowthSpeedBonusRarity = Rarity.Rare,
            AdditiveHairGrowthSpeedBonus = 100.0,
            
            ScalingFactorBonusRarity = Rarity.Special,
            MultiplicativeScalingFactorBonus = 3.0,
            
            MaxHairBonusRarity = Rarity.Rare,
            MultiplicativeMaxHairBonus = 2.5
        },
        new()
        {
            Name = "Skeleton Man",
            BaseCostPerHour = 100,
            
            HairGrowthSpeedBonusRarity = Rarity.Rare,
            AdditiveHairGrowthSpeedBonus = 100.0,
            
            ScalingFactorBonusRarity = Rarity.Special,
            MultiplicativeScalingFactorBonus = 3.0,
            
            MaxHairBonusRarity = Rarity.Rare,
            MultiplicativeMaxHairBonus = 2.5
        },
        new()
        {
            Name = "Saeryn",
            BaseCostPerHour = 100,
            
            HairGrowthSpeedBonusRarity = Rarity.Special,
            AdditiveHairGrowthSpeedBonus = 125.0,
            
            ScalingFactorBonusRarity = Rarity.Rare,
            MultiplicativeScalingFactorBonus = 2.5, 
            
            MaxHairBonusRarity = Rarity.Rare,
            MultiplicativeMaxHairBonus = 2.5
        },
    ];

    public static Stylist GetRandomPreset(Random random)
    {
        return Presets[random.Next(Presets.Count)];
    }
}