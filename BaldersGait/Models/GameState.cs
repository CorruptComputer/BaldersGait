using System.Text.Json.Serialization;
using BaldersGait.Models.BarberShop;
using BaldersGait.Models.Enums;
using BaldersGait.Models.Stylists;

namespace BaldersGait.Models;

[Serializable]
public class GameState
{
    [JsonIgnore] 
    private static readonly Random Random = new();
    
    #region Saved State
    public double HairCollected { get; set; } = 0;

    public double MoneyCollected { get; set; } = 0;

    #region Barber Shop
    public List<BarberShopChair> Chairs { get; set; } =
    [
        new()
        {
            ChairNumber = ChairNumbers.One,
            Unlocked = true
        },
        new()
        {
            ChairNumber = ChairNumbers.Two,
        },
        new()
        {
            ChairNumber = ChairNumbers.Three,
        },
        new()
        {
            ChairNumber = ChairNumbers.Four,
        },
        new()
        {
            ChairNumber = ChairNumbers.Five,
        },
        new()
        {
            ChairNumber = ChairNumbers.Six,
        },
        new()
        {
            ChairNumber = ChairNumbers.Seven,
        },
        new()
        {
            ChairNumber = ChairNumbers.Eight,
        }

    ];
    #endregion

    #region Upgrades Shop
    public bool ClippersPurchased { get; set; } = false;
    public bool WigShopPurchased { get; set; } = false;
    public bool StylistsPurchased { get; set; } = false;
    
    public int HairGrowthUpgrades { get; set; } = 0;

    public int ScalingFactorUpgrades { get; set; } = 0;
    
    public int MaxHairUpgrades { get; set; } = 0;
    #endregion

    #region Stylists
    public List<Stylist> StylistsForHire { get; set; } = Stylist.RollNRandomStylists(Random, 0, 4);
    
    public int StylistsForHireRerollCount { get; set; } = 0;
    #endregion
    #endregion

    #region Calculated State
    [JsonIgnore]
    public double BaseHairPerTick => Math.Round(0.01 * (HairGrowthUpgrades + 1), 3);

    [JsonIgnore] 
    public bool IsMoneyVisible => WigShopPurchased;

    [JsonIgnore]
    public bool IsStylistsButtonVisible => StylistsPurchased;
    
    [JsonIgnore]
    public bool IsWigShopButtonVisible => WigShopPurchased;
    
    #endregion
    
    public void TickMe()
    {
        Parallel.ForEach(Chairs.Where(x => x.Unlocked), seat =>
        {
            double hairGrowth = seat.GetHairGrowthWithScalingFactor(BaseHairPerTick, ScalingFactorUpgrades);
            double maxHairLength = seat.GetMaxHairLength(MaxHairUpgrades);

            // If we are making more per tick than we can hold
            if (hairGrowth > maxHairLength)
            {
                seat.HairLength = maxHairLength;

                if (ClippersPurchased)
                {
                    HairCollected = Math.Round(HairCollected + seat.HairLength, 3);
                }

                return;
            }

            // If we are full
            if (seat.HairLength >= maxHairLength)
            {
                if (ClippersPurchased)
                {
                    HairCollected = Math.Round(HairCollected + maxHairLength, 3);
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