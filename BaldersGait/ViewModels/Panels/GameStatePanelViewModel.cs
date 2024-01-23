using Avalonia.Media;
using BaldersGait.Services.Interface;

namespace BaldersGait.ViewModels.Panels;

public class GameStatePanelViewModel(IStateService stateService) : PanelBase
{
    public override string PanelName => "Game State";
    public override IBrush PanelButtonBackgroundColor { get; } = Brushes.DarkRed;


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