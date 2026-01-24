using Avalonia.Media;
using BaldersGait.Core.Services.Interface;

namespace BaldersGait.UI.ViewModels.Panels;

public class GameStatePanelViewModel(IStateService stateService, bool autoRefreshUi = true) : PanelBase(autoRefreshUi)
{
    public override string PanelName => "Game State";
    public override IBrush PanelButtonBackgroundColor { get; } = Brushes.DarkRed;

    public override bool IsVisible => true;


    #region Click Events
    public bool SaveState_Click()
    {
        stateService.SaveState();
        return true;
    }

    public bool LoadState_Click()
    {
        stateService.LoadState();
        return true;
    }

    public bool ResetState_Click()
    {
        stateService.LoadState(resetState: true);
        return true;
    }
    #endregion

    protected override void RefreshUIFromState()
    {
        // Nothing to do
    }
}