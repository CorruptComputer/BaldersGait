using Avalonia.Media;

namespace BaldersGait.ViewModels.Panels;

public class KnownIssuesPanelViewModel : PanelBase
{
    public override string PanelName => "Known Issues";
    public override IBrush PanelButtonBackgroundColor { get; } = Brushes.SlateGray;
    
    protected override void RefreshUIFromState()
    {
        // Nothing to do
    }
}