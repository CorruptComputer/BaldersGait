using System.Collections.Generic;
using BaldersGait.Services.Interface;
using BaldersGait.ViewModels.Panels;
using ReactiveUI;

namespace BaldersGait.ViewModels.Sidebar;

public class SidebarViewModel(BarberShopPanelViewModel barberShopPanel, GameStatePanelViewModel gameStatePanel, 
    PlannedChangesPanelViewModel plannedChangesPanel, KnownIssuesPanelViewModel knownIssuesPanel,
    IStateService stateService)
    : ViewModelBase
{
    public string HairCollectedLabel => $"Hair collected:\n{stateService.GetBarberShopState().HairCollected:F2}";
    
    public List<SidebarButtonViewModel> Buttons { get; } =
    [
        new(barberShopPanel),
        new(gameStatePanel),
        new(plannedChangesPanel),
        new(knownIssuesPanel)
    ];
    
    protected override void RefreshUIFromState()
    {
        this.RaisePropertyChanged(nameof(HairCollectedLabel));
        this.RaisePropertyChanged(nameof(Buttons));
    }
}