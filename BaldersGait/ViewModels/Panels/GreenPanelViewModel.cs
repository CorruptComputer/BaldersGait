using Avalonia.Media;

namespace BaldersGait.ViewModels.Panels;

public class GreenPanelViewModel : PanelBase
{
    public override string PanelName => "Green";
    public override IBrush PanelButtonBackgroundColor { get; } = Brushes.ForestGreen;
}