using Avalonia.Media;
using BaldersGait.Services.Interface;
using BaldersGait.ViewModels.Panels;
using ReactiveUI;

namespace BaldersGait.ViewModels.Sidebar;

public class SidebarViewModel(
    BarberShopPanelViewModel barberShopPanel,
    UpgradeShopPanelViewModel upgradeShopPanel,
    CompanyPanelViewModel companyPanel,
    GameStatePanelViewModel gameStatePanel,
    IStateService stateService,
    bool autoRefreshUi = true) : ViewModelBase(autoRefreshUi)
{
    public string HairCollectedLabel => $"Hair collected:\n{stateService.SavedState.HairCollected:#,##0.##}\"";

    public bool MoneyCollectedVisible => stateService.CalculatedState.IsMoneyVisible;
    public string MoneyCollectedLabel => $"Money collected:\n${stateService.SavedState.MoneyCollected:#,##0.##}";

    public List<SidebarButtonViewModel> Buttons { get; } =
    [
        new(barberShopPanel, autoRefreshUi),
        new(upgradeShopPanel, autoRefreshUi),
        new(companyPanel, autoRefreshUi),
        new(gameStatePanel, autoRefreshUi),
        new("Roadmap \ud83d\udd17", Brushes.DarkSlateGray, "https://github.com/users/CorruptComputer/projects/3/views/1", autoRefreshUi),
        new("Source \ud83d\udd17", Brushes.DarkSlateGray, "https://github.com/CorruptComputer/BaldersGait", autoRefreshUi)
    ];

    protected override void RefreshUIFromState()
    {
        this.RaisePropertyChanged(nameof(HairCollectedLabel));
        this.RaisePropertyChanged(nameof(MoneyCollectedVisible));
        this.RaisePropertyChanged(nameof(MoneyCollectedLabel));
        this.RaisePropertyChanged(nameof(Buttons));
    }
}