using BaldersGait.Models.Enums;

namespace BaldersGait.Models.Stylists;

[Serializable]
public class Stylist
{
    public bool Preset { get; init; } = false;
    public string Name { get; init; } = string.Empty;
    public double BaseCostPerHour { get; init; } = 0;
    public Rarity OverallRarity { get; init; } = Rarity.Common;
    
    // Hair Growth
    public Rarity HairGrowthSpeedBonusRarity { get; init; } = Rarity.Common;
    public double AdditiveHairGrowthSpeedBonus { get; init; } = 0;
    
    // Scaling Factor
    public Rarity ScalingFactorBonusRarity { get; init; } = Rarity.Common;
    public double MultiplicativeScalingFactorBonus { get; init; } = 0;
    
    // Max Hair
    public Rarity MaxHairBonusRarity { get; init; } = Rarity.Common;
    public double MultiplicativeMaxHairBonus { get; init; } = 0;

    public static List<Stylist> RollNRandomStylists(Random random, int stylistsForHireRerollCount, int stylistCount)
    {
        List<Stylist> stylists = [];
        
        for (int i = 0; i < stylistCount; i++)
        {
            stylists.Add(RollRandomStylist(random, stylistsForHireRerollCount));
        }
        
        return stylists;
    }

    public static Stylist RollRandomStylist(Random random, int stylistsForHireRerollCount)
    {
        Rarity hairGrowthSpeedRarity = RollRandomRarity(random, stylistsForHireRerollCount);
        Rarity scalingFactorBonusRarity = RollRandomRarity(random, stylistsForHireRerollCount);
        Rarity maxHairBonusRarity = RollRandomRarity(random, stylistsForHireRerollCount);

        if (hairGrowthSpeedRarity == Rarity.Rare
            && scalingFactorBonusRarity == Rarity.Rare
            && maxHairBonusRarity == Rarity.Rare)
        {
            return PresetStylists.GetRandomPreset(random);
        }
        
        List<int> rarities = [(int) hairGrowthSpeedRarity, (int) scalingFactorBonusRarity, (int) maxHairBonusRarity];
        Rarity overallRarity = (Rarity)rarities.Max();

        double additiveHairGrowthSpeedBonus = Math.Round(hairGrowthSpeedRarity switch
        {
            Rarity.Common => random.Next(25),
            Rarity.Uncommon => random.Next(25, 60),
            Rarity.Rare => random.Next(60, 100),
            _ => throw new BaldersGaitException("Rarity out of range", false)
        } / 100.0, 3);
        
        double multiplicativeScalingFactorBonus = Math.Round(scalingFactorBonusRarity switch
        {
            Rarity.Common => random.Next(2, 4),
            Rarity.Uncommon => random.Next(3, 5),
            Rarity.Rare => random.Next(5, 7),
            _ => throw new BaldersGaitException("Rarity out of range", false)
        } / 2.0, 3);
        
        double multiplicativeMaxHairBonus = Math.Round(maxHairBonusRarity switch
        {
            Rarity.Common => random.Next(2, 4),
            Rarity.Uncommon => random.Next(3, 5),
            Rarity.Rare => random.Next(5, 7),
            _ => throw new BaldersGaitException("Rarity out of range", false)
        } / 2.0, 3);
        
        return new()
        {
            Preset = false,
            Name = "Testy McTesterson", // TODO: Figure out names later
            BaseCostPerHour = random.Next(30 * (int)overallRarity),
            OverallRarity = overallRarity,
            HairGrowthSpeedBonusRarity = hairGrowthSpeedRarity,
            AdditiveHairGrowthSpeedBonus = additiveHairGrowthSpeedBonus,
            ScalingFactorBonusRarity = scalingFactorBonusRarity,
            MultiplicativeScalingFactorBonus = multiplicativeScalingFactorBonus,
            MaxHairBonusRarity = maxHairBonusRarity,
            MultiplicativeMaxHairBonus = multiplicativeMaxHairBonus
        };
    }

    private static Rarity RollRandomRarity(Random random, int stylistsForHireRerollCount)
    {
        const int baseCommonChance = 700;
        const int baseUncommonChance = 290;
        const int baseRareChance = 10;
        
        int actualUncommonChance = baseUncommonChance + (stylistsForHireRerollCount * 10);
        int actualRareChance = baseRareChance + stylistsForHireRerollCount;
        
        int totalChance = baseCommonChance + actualUncommonChance + actualRareChance;
        int rarityRoll = random.Next(totalChance);
        
        if (rarityRoll < baseCommonChance)
        {
            return Rarity.Common;
        }
        
        return rarityRoll < baseCommonChance + actualUncommonChance 
            ? Rarity.Uncommon 
            : Rarity.Rare;
    }
}