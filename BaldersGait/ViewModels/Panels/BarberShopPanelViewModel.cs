using Avalonia.Media;
using BaldersGait.Models.Enums;
using BaldersGait.Services.Interface;
using BaldersGait.ViewModels.Panels.BarberShop;

namespace BaldersGait.ViewModels.Panels;

public class BarberShopPanelViewModel(IStateService stateService) : PanelBase
{
    public override string PanelName => "Barber Shop";
    public override IBrush PanelButtonBackgroundColor { get; } = Brushes.SteelBlue;
    
    public override bool IsVisible => true;

    public BarberShopChairViewModel ChairOne { get; init; } = new(stateService, ChairNumbers.One);
    public BarberShopChairViewModel ChairTwo { get; init; } = new(stateService, ChairNumbers.Two);
    public BarberShopChairViewModel ChairThree { get; init; } = new(stateService, ChairNumbers.Three);
    public BarberShopChairViewModel ChairFour { get; init; } = new(stateService, ChairNumbers.Four);
    public BarberShopChairViewModel ChairFive { get; init; } = new(stateService, ChairNumbers.Five);
    public BarberShopChairViewModel ChairSix { get; init; } = new(stateService, ChairNumbers.Six);
    public BarberShopChairViewModel ChairSeven { get; init; } = new(stateService, ChairNumbers.Seven);
    public BarberShopChairViewModel ChairEight { get; init; } = new(stateService, ChairNumbers.Eight);

    protected override void RefreshUIFromState()
    {
        // Nothing to do
    }
}