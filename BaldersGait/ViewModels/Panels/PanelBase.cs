using Avalonia.Media;

namespace BaldersGait.ViewModels.Panels;

public abstract class PanelBase : ViewModelBase
{
    public abstract string PanelName { get; }

    public abstract IBrush PanelButtonBackgroundColor { get; }
}