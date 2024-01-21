using Avalonia.Media;
using BaldersGait.Models.State.BarberShop;
using BaldersGait.Services.Interface;

namespace BaldersGait.ViewModels.Panels.BarberShop;

public class BarberShopBottomViewModel : ViewModelBase
{
    public BarberShopChairViewModel SeatOne { get; init; }
    public BarberShopChairViewModel SeatTwo { get; init; }
    public BarberShopChairViewModel SeatThree { get; init; }
    public BarberShopChairViewModel SeatFour { get; init; }
    public BarberShopChairViewModel SeatFive { get; init; }
    public BarberShopChairViewModel SeatSix { get; init; }
    public BarberShopChairViewModel SeatSeven { get; init; }
    public BarberShopChairViewModel SeatEight { get; init; }
    
    public BarberShopBottomViewModel(IStateService stateService)
    {
        SeatOne = new(stateService, SeatNumber.One);
        SeatTwo = new(stateService, SeatNumber.Two);
        SeatThree = new(stateService, SeatNumber.Three);
        SeatFour = new(stateService, SeatNumber.Four);
        SeatFive = new(stateService, SeatNumber.Five);
        SeatSix = new(stateService, SeatNumber.Six);
        SeatSeven = new(stateService, SeatNumber.Seven);
        SeatEight = new(stateService, SeatNumber.Eight);
    }
}