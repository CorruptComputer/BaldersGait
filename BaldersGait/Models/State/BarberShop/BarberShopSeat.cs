using System;

namespace BaldersGait.Models.State.BarberShop;

[Serializable]
public class BarberShopSeat
{
    public SeatNumber SeatNumber { get; set; } = SeatNumber.Unknown;
    public bool Unlocked { get; set; } = false;
    
    public double HairLength { get; set; } = 0;
}