using Avalonia.Media;
using BaldersGait.Models.Enums;
using BaldersGait.Services.Interface;
using ReactiveUI;

namespace BaldersGait.ViewModels.Panels;

public class UpgradeShopPanelViewModel(IStateService stateService) : PanelBase
{
    public override string PanelName => "Upgrade Shop";
    public override IBrush PanelButtonBackgroundColor { get; } = Brushes.MediumSeaGreen;

    public override bool IsVisible => true;
    
    // Purchase Clippers
    public bool PurchaseClippersButtonVisible => !stateService.GetGameState().ClippersPurchased;
    public string PurchaseClippersButtonLabel => stateService.GetGameState().ClippersPurchased ? "Clippers (purchased)" : "Purchase Clippers (500 Hair)";
    
    // Purchase Wig Shop
    public bool PurchaseWigShopButtonVisible => stateService.GetGameState().ClippersPurchased &&
                                                !stateService.GetGameState().WigShopPurchased;
    public string PurchaseWigShopButtonLabel => stateService.GetGameState().WigShopPurchased ? "Wigs & Co. (purchased)" : "Purchase Wigs & Co. (10000 hair)";
    
    // Purchase Stylists
    public bool PurchaseStylistsButtonVisible => stateService.GetGameState().WigShopPurchased &&
                                                 !stateService.GetGameState().StylistsPurchased;
    public string PurchaseStylistsButtonLabel => stateService.GetGameState().StylistsPurchased ? "Stylists (purchased)" : "Purchase Stylists ($200)";
    
    // Unlock Chair
    private int ChairsUnlocked => stateService.GetGameState().Chairs.Count(s => s.Unlocked);
    private int UnlockChairCost => 200 * (ChairsUnlocked);
    public string UnlockSeatButtonLabel => ChairsUnlocked == 8 ? "All Chairs Unlocked" : $"Unlock Chair {(ChairNumbers)ChairsUnlocked + 1} ({UnlockChairCost} hair)";

    // Increase Growth
    private int IncreaseGrowthCost => 100 * (stateService.GetGameState().HairGrowthUpgrades + 1);
    public string IncreaseGrowthButtonLabel => $"[{stateService.GetGameState().HairGrowthUpgrades}] Increase Growth ({IncreaseGrowthCost} Hair)";

    // Increase Scaling Factor
    private int IncreaseScalingFactorCost => 500 * (stateService.GetGameState().ScalingFactorUpgrades + 1);
    public string IncreaseScalingFactorButtonLabel => $"[{stateService.GetGameState().ScalingFactorUpgrades}] Increase Scaling Factor ({IncreaseScalingFactorCost} Hair)";
    
    // Increase Max Hair
    private int IncreaseMaxHairCost => 1000 * (stateService.GetGameState().MaxHairUpgrades + 1);
    public string IncreaseMaxHairButtonLabel => $"[{stateService.GetGameState().MaxHairUpgrades}] Increase Max Hair ({IncreaseMaxHairCost} Hair)";
    
    #region Click Events
    public bool PurchaseClippers_Click()
    {
        //if (HairCut < 500)
        //{
        //    return false;
        //}

        //stateService.GetBarberShopState().HairCollected = Math.Round(stateService.GetBarberShopState().HairCollected - 500, 3);
        stateService.GetGameState().ClippersPurchased = true;

        return false;
    }
    
    public bool PurchaseWigShop_Click()
    {
        if (stateService.GetGameState().WigShopPurchased || stateService.GetGameState().HairCollected < 10000)
        {
            return false;
        }

        stateService.GetGameState().HairCollected = Math.Round(stateService.GetGameState().HairCollected - 10000, 2);
        stateService.GetGameState().WigShopPurchased = true;

        return true;
    }
    
    public bool PurchaseStylists_Click()
    {
        if (stateService.GetGameState().StylistsPurchased || stateService.GetGameState().MoneyCollected < 200)
        {
            return false;
        }

        stateService.GetGameState().MoneyCollected = Math.Round(stateService.GetGameState().MoneyCollected - 200, 2);
        stateService.GetGameState().StylistsPurchased = true;

        return true;
    }
    
    public bool UnlockChair_Click()
    {
        int chairCount = stateService.GetGameState().Chairs.Count;
        if (ChairsUnlocked == chairCount || stateService.GetGameState().HairCollected < UnlockChairCost)
        {
            return false;
        }

        for (int i = ChairsUnlocked; i < chairCount; i++)
        {
            if (stateService.GetGameState().Chairs[i].Unlocked)
            {
                continue;
            }
            
            stateService.GetGameState().HairCollected = Math.Round(stateService.GetGameState().HairCollected - UnlockChairCost, 3);
            stateService.GetGameState().Chairs[i].Unlocked = true;
            return true;
        }

        return false;
    }
    
    public bool IncreaseGrowth_Click()
    {
        if (stateService.GetGameState().HairCollected < IncreaseGrowthCost)
        {
            return false;
        }

        stateService.GetGameState().HairCollected = Math.Round(stateService.GetGameState().HairCollected - IncreaseGrowthCost, 3);
        stateService.GetGameState().HairGrowthUpgrades++;

        return true;
    }
    
    public bool IncreaseScalingFactor_Click()
    {
        if (stateService.GetGameState().HairCollected < IncreaseScalingFactorCost)
        {
            return false;
        }

        stateService.GetGameState().HairCollected = Math.Round(stateService.GetGameState().HairCollected - IncreaseScalingFactorCost, 3);
        stateService.GetGameState().ScalingFactorUpgrades++;

        return true;
    }
    
    public bool IncreaseMaxHair_Click()
    {
        if (stateService.GetGameState().HairCollected < IncreaseMaxHairCost)
        {
            return false;
        }

        stateService.GetGameState().HairCollected = Math.Round(stateService.GetGameState().HairCollected - IncreaseMaxHairCost, 3);
        stateService.GetGameState().MaxHairUpgrades++;

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