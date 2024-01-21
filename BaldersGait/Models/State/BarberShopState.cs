using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using BaldersGait.Consts;
using BaldersGait.Models.State.BarberShop;

namespace BaldersGait.Models.State;

[Serializable]
public class BarberShopState
{
    public List<BarberShopSeat> Seats { get; set; } =
    [
        new()
        {
            SeatNumber = SeatNumber.One,
            Unlocked = true
        },
        new()
        {
            SeatNumber = SeatNumber.Two,
        },
        new()
        {
            SeatNumber = SeatNumber.Three,
        },
        new()
        {
            SeatNumber = SeatNumber.Four,
        },
        new()
        {
            SeatNumber = SeatNumber.Five,
        },
        new()
        {
            SeatNumber = SeatNumber.Six,
        },
        new()
        {
            SeatNumber = SeatNumber.Seven,
        },
        new()
        {
            SeatNumber = SeatNumber.Eight,
        }

    ];

    public int HairGrowthUpgrades { get; set; } = 0;
    
    public int ReduceReductionUpgrades { get; set; } = 0;
    
    public double HairCollected { get; set; } = 0;
    
    public bool ClippersPurchased { get; set; } = false;
    
    [JsonIgnore]
    public double HairGrowth => Math.Round(BarberShopConsts.BaseHairGrowthRate * (HairGrowthUpgrades+1), 3);
    
    public void TickMe()
    {
        Parallel.ForEach(Seats.Where(x => x.Unlocked), seat =>
        {
            if (seat.HairLength < BarberShopConsts.MaxHairLength)
            {
                double growthRate = HairGrowth * (seat.SeatNumber == SeatNumber.One ? 1 : Math.Pow(BarberShopConsts.BaseReduction, (int)seat.SeatNumber));
                double newLength = Math.Round(seat.HairLength + growthRate, 3);
                if (newLength >= BarberShopConsts.MaxHairLength)
                {
                    if (ClippersPurchased)
                    {
                        HairCollected = Math.Round(HairCollected + BarberShopConsts.MaxHairLength, 3);
                        newLength = 0;
                    }
                    else
                    {
                        newLength = BarberShopConsts.MaxHairLength;
                    }
                }
            
                seat.HairLength = newLength;
            }
        });
    }
}