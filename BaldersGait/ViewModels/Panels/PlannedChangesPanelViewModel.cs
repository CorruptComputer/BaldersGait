using Avalonia.Media;

namespace BaldersGait.ViewModels.Panels;

public class PlannedChangesPanelViewModel : PanelBase
{
    public override string PanelName => "Planned Changes";
    public override IBrush PanelButtonBackgroundColor { get; } = Brushes.SlateGray;

    protected override void RefreshUIFromState()
    {
        // Nothing to do
    }
}