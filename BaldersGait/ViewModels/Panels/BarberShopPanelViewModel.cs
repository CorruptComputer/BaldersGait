using Avalonia.Media;
using BaldersGait.Services.Interface;
using BaldersGait.ViewModels.Panels.BarberShop;

namespace BaldersGait.ViewModels.Panels;

public class BarberShopPanelViewModel(BarberShopTopViewModel topView, BarberShopBottomViewModel bottomView)
    : PanelBase
{
    public override string PanelName => "Barber Shop";
    public override IBrush PanelButtonBackgroundColor { get; } = Brushes.SteelBlue;

    public BarberShopTopViewModel TopView { get; init; } = topView;

    public BarberShopBottomViewModel BottomView { get; init; } = bottomView;

    protected override void RefreshUIFromState()
    {
        // Nothing to do
    }
}