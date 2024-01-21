using Avalonia.Media;

namespace BaldersGait.ViewModels.Panels;

public class BluePanelViewModel : PanelBase
{
    public override string PanelName => "Blue";
    public override IBrush PanelButtonBackgroundColor { get; } = Brushes.DodgerBlue;
}