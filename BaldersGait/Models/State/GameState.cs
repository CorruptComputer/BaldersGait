using System;

namespace BaldersGait.Models.State;

[Serializable]
public class GameState
{
    public BarberShopState BarberShopState { get; set; } = new();

    public void TickMe()
    {
        BarberShopState.TickMe();
    }
}