using System.Text.Json.Serialization;
using BaldersGait.Models.State.BarberShop;

namespace BaldersGait.Models.State;

[Serializable]
public class BarberShopState
{
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

    public int HairGrowthUpgrades { get; set; } = 0;

    public int ChairScalingFactorUpgrades { get; set; } = 0;

    public double HairCollected { get; set; } = 0;

    public bool ClippersPurchased { get; set; } = false;


    [JsonIgnore]
    public double BaseHairPerTick => Math.Round(0.01 * (HairGrowthUpgrades + 1), 3);

    public void TickMe()
    {
        Parallel.ForEach(Chairs.Where(x => x.Unlocked), seat =>
        {
            // TODO: Passing in 'this' works but just feels awful
            double hairGrowth = seat.GetHairGrowthWithScalingFactor(BaseHairPerTick);
            double maxHairLength = seat.GetMaxHairLength();

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