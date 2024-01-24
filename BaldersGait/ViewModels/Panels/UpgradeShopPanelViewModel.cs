using Avalonia.Media;
using BaldersGait.Models.State.BarberShop;
using BaldersGait.Services.Interface;
using ReactiveUI;

namespace BaldersGait.ViewModels.Panels;

public class UpgradeShopPanelViewModel(IStateService stateService) : PanelBase
{
    public override string PanelName => "Upgrade Shop";
    public override IBrush PanelButtonBackgroundColor { get; } = Brushes.MediumSeaGreen;

    // Increase Growth
    private int IncreaseGrowthCost => 100 * (stateService.GetBarberShopState().HairGrowthUpgrades + 1);
    public string IncreaseGrowthButtonLabel => $"[{stateService.GetBarberShopState().HairGrowthUpgrades}] Increase Growth ({IncreaseGrowthCost} Hair)";

    // Unlock Chair
    private int ChairsUnlocked => stateService.GetBarberShopState().Chairs.Count(s => s.Unlocked) + 1;
    private int UnlockChairCost => 200 * (ChairsUnlocked - 1);
    public string UnlockSeatButtonLabel => ChairsUnlocked == 8 ? "All Chairs Unlocked" : $"Unlock Chair {(ChairNumbers)ChairsUnlocked} ({UnlockChairCost} hair)";

    // Increase Scaling Factor
    private int IncreaseScalingFactorCost => 500 * (stateService.GetBarberShopState().ChairScalingFactorUpgrades + 1);
    public string IncreaseScalingFactorButtonLabel => $"[{stateService.GetBarberShopState().ChairScalingFactorUpgrades}] Increase Scaling Factor ({IncreaseScalingFactorCost} Hair)";

    // Purchase Clippers
    public string PurchaseClippersButtonLabel => stateService.GetBarberShopState().ClippersPurchased ? "Clippers (purchased)" : "Purchase Clippers (500 Hair)";

    #region Click Events
    public bool PurchaseClippers_Click()
    {
        //if (HairCut < 500)
        //{
        //    return false;
        //}

        //stateService.GetBarberShopState().HairCollected = Math.Round(stateService.GetBarberShopState().HairCollected - 500, 3);
        stateService.GetBarberShopState().ClippersPurchased = true;

        return false;
    }

    public bool IncreaseScalingFactor_Click()
    {
        if (stateService.GetBarberShopState().HairCollected < IncreaseScalingFactorCost)
        {
            return false;
        }

        stateService.GetBarberShopState().HairCollected = Math.Round(stateService.GetBarberShopState().HairCollected - IncreaseScalingFactorCost, 3);
        stateService.GetBarberShopState().ChairScalingFactorUpgrades++;

        return true;
    }

    public bool UnlockChair_Click()
    {
        if (stateService.GetBarberShopState().HairCollected < UnlockChairCost)
        {
            return false;
        }

        for (int i = stateService.GetBarberShopState().Chairs.Count(seat => seat.Unlocked); i < stateService.GetBarberShopState().Chairs.Count; i++)
        {
            if (!stateService.GetBarberShopState().Chairs[i].Unlocked)
            {
                stateService.GetBarberShopState().HairCollected = Math.Round(stateService.GetBarberShopState().HairCollected - UnlockChairCost, 3);
                stateService.GetBarberShopState().Chairs[i].Unlocked = true;
                return true;
            }
        }

        return false;
    }

    public bool IncreaseGrowth_Click()
    {
        if (stateService.GetBarberShopState().HairCollected < IncreaseGrowthCost)
        {
            return false;
        }

        stateService.GetBarberShopState().HairCollected = Math.Round(stateService.GetBarberShopState().HairCollected - IncreaseGrowthCost, 3);
        stateService.GetBarberShopState().HairGrowthUpgrades++;

        return true;
    }
    #endregion

    protected override void RefreshUIFromState()
    {
        this.RaisePropertyChanged(nameof(PurchaseClippersButtonLabel));

        this.RaisePropertyChanged(nameof(IncreaseScalingFactorButtonLabel));
        this.RaisePropertyChanged(nameof(UnlockSeatButtonLabel));
        this.RaisePropertyChanged(nameof(IncreaseGrowthButtonLabel));
    }
}