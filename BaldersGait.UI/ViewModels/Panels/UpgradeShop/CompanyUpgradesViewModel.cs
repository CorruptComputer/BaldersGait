using BaldersGait.Core.Services.Interface;

namespace BaldersGait.UI.ViewModels.Panels.UpgradeShop;

public class CompanyUpgradesViewModel(IStateService stateService, bool autoRefreshUi = true) : ViewModelBase(autoRefreshUi)
{
    protected override void RefreshUIFromState()
    {
        // TODO: Delete this once this is implemented
        if (stateService.CalculatedState.IsMoneyVisible)
        {

        }
    }
}