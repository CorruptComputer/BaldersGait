using Avalonia.Media;
using BaldersGait.Core.Models.Enums;
using BaldersGait.Core.Services.Interface;
using BaldersGait.UI.ViewModels.Panels.BarberShop;

namespace BaldersGait.UI.ViewModels.Panels;

public class BarberShopPanelViewModel(IStateService stateService, bool autoRefreshUi = true) : PanelBase(autoRefreshUi)
{
    public override string PanelName => "Barber Shop";
    public override IBrush PanelButtonBackgroundColor { get; } = Brushes.SteelBlue;

    public override bool IsVisible => true;

    public BarberShopChairViewModel ChairOne { get; init; } = new(stateService, ChairNumbers.One, autoRefreshUi);
    public BarberShopChairViewModel ChairTwo { get; init; } = new(stateService, ChairNumbers.Two, autoRefreshUi);
    public BarberShopChairViewModel ChairThree { get; init; } = new(stateService, ChairNumbers.Three, autoRefreshUi);
    public BarberShopChairViewModel ChairFour { get; init; } = new(stateService, ChairNumbers.Four, autoRefreshUi);
    public BarberShopChairViewModel ChairFive { get; init; } = new(stateService, ChairNumbers.Five, autoRefreshUi);
    public BarberShopChairViewModel ChairSix { get; init; } = new(stateService, ChairNumbers.Six, autoRefreshUi);
    public BarberShopChairViewModel ChairSeven { get; init; } = new(stateService, ChairNumbers.Seven, autoRefreshUi);
    public BarberShopChairViewModel ChairEight { get; init; } = new(stateService, ChairNumbers.Eight, autoRefreshUi);

    protected override void RefreshUIFromState()
    {
        // Nothing to do
    }
}