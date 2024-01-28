using System.Diagnostics;
using Avalonia.Media;
using BaldersGait.Models.Enums;
using BaldersGait.ViewModels.Panels;
using ReactiveUI;
using Serilog;

namespace BaldersGait.ViewModels.Sidebar;

public class SidebarButtonViewModel : ViewModelBase
{
    public string ButtonText { get; init; }

    public IBrush BackgroundColor { get; init; }

    public ButtonTypes ButtonType { get; init; }

    public bool IsVisible => PanelToOpen?.IsVisible ?? true;

    public PanelBase? PanelToOpen { get; init; }

    public string? LinkUrl { get; init; }

    public SidebarButtonViewModel(PanelBase panelToOpen)
    {
        ButtonType = ButtonTypes.Panel;
        PanelToOpen = panelToOpen;
        ButtonText = panelToOpen.PanelName;
        BackgroundColor = panelToOpen.PanelButtonBackgroundColor;
    }

    public SidebarButtonViewModel(string text, IBrush background, string linkUrl)
    {
        ButtonType = ButtonTypes.Link;
        ButtonText = text;
        BackgroundColor = background;
        LinkUrl = linkUrl;
    }

    public bool OnClick()
    {
        switch (ButtonType)
        {
            case ButtonTypes.Panel:
                if (MainWindowViewModel.Current == null || PanelToOpen == null)
                {
                    return false;
                }

                MainWindowViewModel.Current.CurrentPanel = PanelToOpen;
                Log.Information($"Opened panel: {PanelToOpen.PanelName}");
                break;
            case ButtonTypes.Link:
                ProcessStartInfo psi = new()
                {
                    FileName = LinkUrl,
                    UseShellExecute = true
                };

                Process.Start(psi);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        return true;
    }

    protected override void RefreshUIFromState()
    {
        this.RaisePropertyChanged(nameof(ButtonText));
        this.RaisePropertyChanged(nameof(BackgroundColor));
        this.RaisePropertyChanged(nameof(IsVisible));
    }
}