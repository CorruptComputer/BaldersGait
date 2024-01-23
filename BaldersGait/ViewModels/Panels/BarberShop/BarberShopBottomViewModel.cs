using BaldersGait.Models.State.BarberShop;
using BaldersGait.Services.Interface;

namespace BaldersGait.ViewModels.Panels.BarberShop;

public class BarberShopBottomViewModel(IStateService stateService) : ViewModelBase
{
    public BarberShopChairViewModel SeatOne { get; init; } = new(stateService, ChairNumbers.One);
    public BarberShopChairViewModel SeatTwo { get; init; } = new(stateService, ChairNumbers.Two);
    public BarberShopChairViewModel SeatThree { get; init; } = new(stateService, ChairNumbers.Three);
    public BarberShopChairViewModel SeatFour { get; init; } = new(stateService, ChairNumbers.Four);
    public BarberShopChairViewModel SeatFive { get; init; } = new(stateService, ChairNumbers.Five);
    public BarberShopChairViewModel SeatSix { get; init; } = new(stateService, ChairNumbers.Six);
    public BarberShopChairViewModel SeatSeven { get; init; } = new(stateService, ChairNumbers.Seven);
    public BarberShopChairViewModel SeatEight { get; init; } = new(stateService, ChairNumbers.Eight);

    protected override void RefreshUIFromState()
    {
        // Nothing to do
    }
}