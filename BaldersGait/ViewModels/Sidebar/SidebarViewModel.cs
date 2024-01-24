using Avalonia.Media;
using BaldersGait.Services.Interface;
using BaldersGait.ViewModels.Panels;
using ReactiveUI;

namespace BaldersGait.ViewModels.Sidebar;

public class SidebarViewModel(BarberShopPanelViewModel barberShopPanel, UpgradeShopPanelViewModel upgradeShopPanel,
    GameStatePanelViewModel gameStatePanel, IStateService stateService) : ViewModelBase
{
    public string HairCollectedLabel => $"Hair collected:\n{stateService.GetBarberShopState().HairCollected:F2}";

    public List<SidebarButtonViewModel> Buttons { get; } =
    [
        new(barberShopPanel),
        new(upgradeShopPanel),
        new(gameStatePanel),
        new("Roadmap \ud83d\udd17", Brushes.DarkSlateGray, "https://github.com/users/CorruptComputer/projects/3/views/1"),
        new("Source \ud83d\udd17", Brushes.DarkSlateGray, "https://github.com/CorruptComputer/BaldersGait")
    ];

    protected override void RefreshUIFromState()
    {
        this.RaisePropertyChanged(nameof(HairCollectedLabel));
        this.RaisePropertyChanged(nameof(Buttons));
    }
}