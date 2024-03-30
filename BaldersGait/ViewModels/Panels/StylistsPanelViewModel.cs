using Avalonia.Media;
using BaldersGait.Services.Interface;

namespace BaldersGait.ViewModels.Panels;

public class StylistsPanelViewModel(IStateService stateService, bool autoRefreshUi = true) : PanelBase(autoRefreshUi)
{
    public override string PanelName => "Stylists";
    public override IBrush PanelButtonBackgroundColor => Brushes.MediumPurple;

    public override bool IsVisible => stateService.CalculatedState.IsStylistsButtonVisible;


    protected override void RefreshUIFromState()
    {
        //
    }
}