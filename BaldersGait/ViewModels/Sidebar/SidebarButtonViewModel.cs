using Avalonia.Media;
using BaldersGait.ViewModels.Panels;
using ReactiveUI;
using Serilog;

namespace BaldersGait.ViewModels.Sidebar;

public class SidebarButtonViewModel(PanelBase panelToOpen) : ViewModelBase
{
    public string ButtonText { get; } = panelToOpen.PanelName;

    public IBrush BackgroundColor { get; private set; } = panelToOpen.PanelButtonBackgroundColor ?? Brushes.SlateGray;

    public PanelBase PanelToOpen { get; } = panelToOpen;

    public bool OnClick()
    {
        if (MainWindowViewModel.Current == null)
        {
            return false;
        }

        MainWindowViewModel.Current.CurrentPanel = PanelToOpen;
        Log.Information($"Opened panel: {PanelToOpen.PanelName}");
        return true;
    }

    protected override void RefreshUIFromState()
    {
        this.RaisePropertyChanged(nameof(BackgroundColor));
    }
}