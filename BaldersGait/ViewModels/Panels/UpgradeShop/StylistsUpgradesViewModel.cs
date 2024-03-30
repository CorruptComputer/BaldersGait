using BaldersGait.Services.Interface;

namespace BaldersGait.ViewModels.Panels.UpgradeShop;

public class StylistsUpgradesViewModel(IStateService stateService, bool autoRefreshUi = true) : ViewModelBase(autoRefreshUi)
{
    protected override void RefreshUIFromState()
    {
        // TODO: Delete this once this is implemented
        if (stateService.CalculatedState.IsMoneyVisible)
        {

        }
    }
}