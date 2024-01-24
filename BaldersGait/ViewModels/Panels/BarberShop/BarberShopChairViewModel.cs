using BaldersGait.Models.State.BarberShop;
using BaldersGait.Services.Interface;
using ReactiveUI;

namespace BaldersGait.ViewModels.Panels.BarberShop;

public class BarberShopChairViewModel(IStateService stateService, ChairNumbers chairNumber) : ViewModelBase
{
    public ChairNumbers ChairNumber { get; } = chairNumber;

    private BarberShopChair ChairState =>
        stateService.GetBarberShopState().Chairs.First(s => s.ChairNumber == ChairNumber);

    public string ChairNumberLabel => $"Chair {ChairNumber}";
    public double HairLength => ChairState.HairLength;
    public double MaxHair => ChairState.GetMaxHairLength();

    public string HairGrowthLabel => $"Hair Growth:\n{stateService.GetBarberShopState().BaseHairPerTick * 60:F2}\" / second";

    public string ScalingFactorLabel => $"Scaling factor:\nx{ChairState.HairGrowthScalingFactor}";

    public string TotalGrowthLabel => $"Total Growth:\n{ChairState.GetHairGrowthWithScalingFactor(stateService.GetBarberShopState().BaseHairPerTick) * 60:F2}\" / second";

    public string CurrentHairLabel => $"Current hair:\n{HairLength:F2}\" / {MaxHair:F2}\"";

    public bool IsChairUnlocked => ChairState.Unlocked;

    public bool ReadyToCollect => ChairState.ReadyToCollect && !ProductionTooHigh;

    public bool ProductionTooHigh => ChairState.IsProductionTooHigh(stateService.GetBarberShopState().BaseHairPerTick);

    public bool CutHair()
    {
        stateService.GetBarberShopState().HairCollected = Math.Round(stateService.GetBarberShopState().HairCollected + ChairState.HairLength, 3);
        ChairState.HairLength = 0;

        return true;
    }

    protected override void RefreshUIFromState()
    {
        this.RaisePropertyChanged(nameof(IsChairUnlocked));

        this.RaisePropertyChanged(nameof(HairLength));
        this.RaisePropertyChanged(nameof(MaxHair));

        this.RaisePropertyChanged(nameof(HairGrowthLabel));
        this.RaisePropertyChanged(nameof(ScalingFactorLabel));
        this.RaisePropertyChanged(nameof(TotalGrowthLabel));
        this.RaisePropertyChanged(nameof(CurrentHairLabel));

        this.RaisePropertyChanged(nameof(ReadyToCollect));
        this.RaisePropertyChanged(nameof(ProductionTooHigh));
    }
}