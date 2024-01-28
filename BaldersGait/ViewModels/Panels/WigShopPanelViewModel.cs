using Avalonia.Media;
using BaldersGait.Services.Interface;

namespace BaldersGait.ViewModels.Panels;

public class WigShopPanelViewModel(IStateService stateService) : PanelBase
{
    public override string PanelName => "Wigs & Co";
    public override IBrush PanelButtonBackgroundColor => Brushes.SandyBrown;

    public override bool IsVisible => stateService.GetGameState().IsWigShopButtonVisible;
    
    protected override void RefreshUIFromState()
    {
        //
    }
}