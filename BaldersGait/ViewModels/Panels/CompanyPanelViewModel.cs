using Avalonia.Media;
using BaldersGait.Services.Interface;

namespace BaldersGait.ViewModels.Panels;

public class CompanyPanelViewModel(IStateService stateService, bool autoRefreshUi = true) : PanelBase(autoRefreshUi)
{
    public override string PanelName => "Wigs & Co";
    public override IBrush PanelButtonBackgroundColor => Brushes.SandyBrown;

    public override bool IsVisible => stateService.CalculatedState.IsWigShopButtonVisible;

    protected override void RefreshUIFromState()
    {
        //
    }
}