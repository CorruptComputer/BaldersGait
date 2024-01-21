using Avalonia.Media;
using BaldersGait.ViewModels.Panels;
using ReactiveUI;
using Serilog;

namespace BaldersGait.ViewModels.Sidebar;

public class SidebarButtonViewModel(PanelBase panelToOpen) : ViewModelBase
{
    public string ButtonText { get; } = panelToOpen.PanelName;

    private IBrush _backgroundColor = panelToOpen.PanelButtonBackgroundColor ?? Brushes.SlateGray;

    public IBrush BackgroundColor
    {
        get => _backgroundColor;
        set => this.RaiseAndSetIfChanged(ref _backgroundColor, value);
    }

    public PanelBase PanelToOpen { get; } = panelToOpen;

    private IBrush OriginalBackgroundColor { get; } = panelToOpen.PanelButtonBackgroundColor ?? Brushes.SlateGray;

    public void SetBackgroundSelected()
    {
        BackgroundColor = Brushes.LightSlateGray;
    }

    public void ResetBackground()
    {
        BackgroundColor = OriginalBackgroundColor;
    }

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
}