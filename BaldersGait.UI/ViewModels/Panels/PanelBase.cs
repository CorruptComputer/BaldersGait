using Avalonia.Media;

namespace BaldersGait.UI.ViewModels.Panels;

public abstract class PanelBase(bool autoRefreshUi) : ViewModelBase(autoRefreshUi)
{
    public abstract string PanelName { get; }

    public abstract IBrush PanelButtonBackgroundColor { get; }

    public abstract bool IsVisible { get; }
}