using Avalonia.Media;
using BaldersGait.Models.Enums;
using BaldersGait.Services.Interface;
using ReactiveUI;

namespace BaldersGait.ViewModels.Panels;

public class UpgradeShopPanelViewModel(IStateService stateService, bool autoRefreshUi = true) : PanelBase(autoRefreshUi)
{
    public override string PanelName => "Upgrade Shop";
    public override IBrush PanelButtonBackgroundColor { get; } = Brushes.MediumSeaGreen;

    public override bool IsVisible => true;

    // Purchase Clippers
    public bool PurchaseClippersButtonVisible => !stateService.SavedState.ClippersPurchased;
    public string PurchaseClippersButtonLabel => stateService.SavedState.ClippersPurchased ? "Clippers (purchased)" : "Purchase Clippers (500 Hair)";

    // Purchase Wig Shop
    public bool PurchaseWigShopButtonVisible => stateService.SavedState.ClippersPurchased &&
                                                !stateService.SavedState.CompanyPurchased;
    public string PurchaseWigShopButtonLabel => stateService.SavedState.CompanyPurchased ? "Wigs & Co. (purchased)" : "Purchase Wigs & Co. (10000 hair)";

    // Purchase Stylists
    public bool PurchaseStylistsButtonVisible => stateService.SavedState.CompanyPurchased &&
                                                 !stateService.SavedState.StylistsPurchased;
    public string PurchaseStylistsButtonLabel => stateService.SavedState.StylistsPurchased ? "Stylists (purchased)" : "Purchase Stylists ($200)";

    // Unlock Chair
    private int ChairsUnlocked => stateService.SavedState.Chairs.Count(s => s.Unlocked);
    private int UnlockChairCost => 200 * (ChairsUnlocked);
    public string UnlockSeatButtonLabel => ChairsUnlocked == 8 ? "All Chairs Unlocked" : $"Unlock Chair {(ChairNumbers)ChairsUnlocked + 1} ({UnlockChairCost} hair)";

    // Increase Growth
    private int IncreaseGrowthCost => 100 * (stateService.SavedState.HairGrowthV1Upgrades + 1);
    public string IncreaseGrowthButtonLabel => $"[{stateService.SavedState.HairGrowthV1Upgrades}] Increase Growth ({IncreaseGrowthCost} Hair)";

    // Increase Scaling Factor
    private int IncreaseScalingFactorCost => 500 * (stateService.SavedState.ScalingFactorV1Upgrades + 1);
    public string IncreaseScalingFactorButtonLabel => $"[{stateService.SavedState.ScalingFactorV1Upgrades}] Increase Scaling Factor ({IncreaseScalingFactorCost} Hair)";

    // Increase Max Hair
    private int IncreaseMaxHairCost => 1000 * (stateService.SavedState.MaxHairV1Upgrades + 1);
    public string IncreaseMaxHairButtonLabel => $"[{stateService.SavedState.MaxHairV1Upgrades}] Increase Max Hair ({IncreaseMaxHairCost} Hair)";

    #region Click Events
    public bool PurchaseClippers_Click()
    {
        //if (HairCut < 500)
        //{
        //    return false;
        //}

        //stateService.GetBarberShopState().HairCollected = Math.Round(stateService.GetBarberShopState().HairCollected - 500, 3);
        stateService.SavedState.ClippersPurchased = true;

        return false;
    }

    public bool PurchaseWigShop_Click()
    {
        if (stateService.SavedState.CompanyPurchased || stateService.SavedState.HairCollected < 10000)
        {
            return false;
        }

        stateService.SavedState.HairCollected = Math.Round(stateService.SavedState.HairCollected - 10000, 2);
        stateService.SavedState.CompanyPurchased = true;

        return true;
    }

    public bool PurchaseStylists_Click()
    {
        if (stateService.SavedState.StylistsPurchased || stateService.SavedState.MoneyCollected < 200)
        {
            return false;
        }

        stateService.SavedState.MoneyCollected = Math.Round(stateService.SavedState.MoneyCollected - 200, 2);
        stateService.SavedState.StylistsPurchased = true;

        return true;
    }

    public bool UnlockChair_Click()
    {
        int chairCount = stateService.SavedState.Chairs.Count;
        if (ChairsUnlocked == chairCount || stateService.SavedState.HairCollected < UnlockChairCost)
        {
            return false;
        }

        for (int i = ChairsUnlocked; i < chairCount; i++)
        {
            if (stateService.SavedState.Chairs[i].Unlocked)
            {
                continue;
            }

            stateService.SavedState.HairCollected = Math.Round(stateService.SavedState.HairCollected - UnlockChairCost, 3);
            stateService.SavedState.Chairs[i].Unlocked = true;
            return true;
        }

        return false;
    }

    public bool IncreaseGrowth_Click()
    {
        if (stateService.SavedState.HairCollected < IncreaseGrowthCost)
        {
            return false;
        }

        stateService.SavedState.HairCollected = Math.Round(stateService.SavedState.HairCollected - IncreaseGrowthCost, 3);
        stateService.SavedState.HairGrowthV1Upgrades++;

        return true;
    }

    public bool IncreaseScalingFactor_Click()
    {
        if (stateService.SavedState.HairCollected < IncreaseScalingFactorCost)
        {
            return false;
        }

        stateService.SavedState.HairCollected = Math.Round(stateService.SavedState.HairCollected - IncreaseScalingFactorCost, 3);
        stateService.SavedState.ScalingFactorV1Upgrades++;

        return true;
    }

    public bool IncreaseMaxHair_Click()
    {
        if (stateService.SavedState.HairCollected < IncreaseMaxHairCost)
        {
            return false;
        }

        stateService.SavedState.HairCollected = Math.Round(stateService.SavedState.HairCollected - IncreaseMaxHairCost, 3);
        stateService.SavedState.MaxHairV1Upgrades++;

        return true;
    }
    #endregion

    protected override void RefreshUIFromState()
    {
        this.RaisePropertyChanged(nameof(PurchaseClippersButtonVisible));
        this.RaisePropertyChanged(nameof(PurchaseWigShopButtonVisible));
        this.RaisePropertyChanged(nameof(PurchaseStylistsButtonVisible));

        this.RaisePropertyChanged(nameof(PurchaseClippersButtonLabel));
        this.RaisePropertyChanged(nameof(PurchaseStylistsButtonLabel));

        this.RaisePropertyChanged(nameof(UnlockSeatButtonLabel));

        this.RaisePropertyChanged(nameof(IncreaseGrowthButtonLabel));
        this.RaisePropertyChanged(nameof(IncreaseScalingFactorButtonLabel));
        this.RaisePropertyChanged(nameof(IncreaseMaxHairButtonLabel));
    }
}