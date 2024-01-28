using Avalonia.Media;
using BaldersGait.Services.Interface;

namespace BaldersGait.ViewModels.Panels;

public class StylistsPanelViewModel(IStateService stateService) : PanelBase
{
    public override string PanelName => "Stylists";
    public override IBrush PanelButtonBackgroundColor => Brushes.MediumPurple;

    public override bool IsVisible => stateService.GetGameState().IsStylistsButtonVisible;
    
    
    protected override void RefreshUIFromState()
    {
        //
    }
}