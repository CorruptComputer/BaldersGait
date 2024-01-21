using System.Collections.ObjectModel;
using BaldersGait.ViewModels.Panels;

namespace BaldersGait.ViewModels.Sidebar;

public class SidebarViewModel(BarberShopPanelViewModel barberShopPanel, GameStatePanelViewModel gameStatePanel, GreenPanelViewModel greenPanel, BluePanelViewModel bluePanel)
    : ViewModelBase
{
    public ObservableCollection<SidebarButtonViewModel> Buttons { get; } =
    [
        new(barberShopPanel),
        new(gameStatePanel),
        new(greenPanel),
        new(bluePanel)
    ];
}