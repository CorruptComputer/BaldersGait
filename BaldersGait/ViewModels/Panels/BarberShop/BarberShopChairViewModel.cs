using BaldersGait.Models.BarberShop;
using BaldersGait.Models.Enums;
using BaldersGait.Services.Interface;
using ReactiveUI;

namespace BaldersGait.ViewModels.Panels.BarberShop;

public class BarberShopChairViewModel(IStateService stateService, ChairNumbers chairNumber, bool autoRefreshUi = true) : ViewModelBase(autoRefreshUi)
{
    public ChairNumbers ChairNumber { get; } = chairNumber;

    private BarberShopChair ChairState =>
        stateService.SavedState.Chairs.First(s => s.ChairNumber == ChairNumber);

    public string ChairNumberLabel => stateService.SavedState.StylistsPurchased ? $"Stylist: {(ChairState.StylistAssigned == null ? "You" : ChairState.StylistAssigned.Name)}\nChair {ChairNumber}" : $"Chair {ChairNumber}";
    public double HairLength => ChairState.HairLength;

    public string BaseGrowthLabel => $"Base Growth:\n{stateService.CalculatedState.BaseHairPerTick * 60:F2}\" / second";
    public double MaxHair => ChairState.GetMaxHairLength(stateService.SavedState.MaxHairV1Upgrades);

    public string ScalingFactorLabel => $"Scaling factor:\nx{ChairState.GetHairGrowthScalingFactor(stateService.SavedState.ScalingFactorV1Upgrades)}";

    public string TotalGrowthLabel => $"Total Growth:\n{ChairState.GetHairGrowthWithScalingFactor(stateService.CalculatedState.BaseHairPerTick, stateService.SavedState.ScalingFactorV1Upgrades) * 60:F2}\" / second";

    public string CurrentHairLabel => $"Current hair:\n{HairLength:F2}\" / {MaxHair:F2}\"";

    public bool IsChairUnlocked => ChairState.Unlocked;

    public bool ReadyToCollect => ChairState.IsReadyToCollect(stateService.SavedState.MaxHairV1Upgrades) && !ProductionTooHigh;

    public bool ProductionTooHigh => ChairState.IsProductionTooHigh(stateService.CalculatedState.BaseHairPerTick, stateService.SavedState.MaxHairV1Upgrades, stateService.SavedState.ScalingFactorV1Upgrades);

    public bool CutHair()
    {
        if (!ReadyToCollect)
        {
            return false;
        }

        stateService.SavedState.HairCollected = Math.Round(stateService.SavedState.HairCollected + ChairState.HairLength, 3);
        ChairState.HairLength = 0;

        return true;
    }

    protected override void RefreshUIFromState()
    {
        this.RaisePropertyChanged(nameof(IsChairUnlocked));

        this.RaisePropertyChanged(nameof(HairLength));
        this.RaisePropertyChanged(nameof(MaxHair));

        this.RaisePropertyChanged(nameof(ScalingFactorLabel));
        this.RaisePropertyChanged(nameof(TotalGrowthLabel));
        this.RaisePropertyChanged(nameof(CurrentHairLabel));

        this.RaisePropertyChanged(nameof(ReadyToCollect));
        this.RaisePropertyChanged(nameof(ProductionTooHigh));
    }
}