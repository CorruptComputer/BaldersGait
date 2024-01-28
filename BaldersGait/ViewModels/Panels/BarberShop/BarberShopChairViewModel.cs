using BaldersGait.Models.BarberShop;
using BaldersGait.Models.Enums;
using BaldersGait.Services.Interface;
using ReactiveUI;

namespace BaldersGait.ViewModels.Panels.BarberShop;

public class BarberShopChairViewModel(IStateService stateService, ChairNumbers chairNumber) : ViewModelBase
{
    public ChairNumbers ChairNumber { get; } = chairNumber;

    private BarberShopChair ChairState =>
        stateService.GetGameState().Chairs.First(s => s.ChairNumber == ChairNumber);

    public string ChairNumberLabel => $"Chair {ChairNumber}";
    public double HairLength => ChairState.HairLength;
    
    public string BaseGrowthLabel => $"Base Growth:\n{stateService.GetGameState().BaseHairPerTick * 60:F2}\" / second";
    public double MaxHair => ChairState.GetMaxHairLength(stateService.GetGameState().MaxHairUpgrades);
    
    public string ScalingFactorLabel => $"Scaling factor:\nx{ChairState.GetHairGrowthScalingFactor(stateService.GetGameState().ScalingFactorUpgrades)}";

    public string TotalGrowthLabel => $"Total Growth:\n{ChairState.GetHairGrowthWithScalingFactor(stateService.GetGameState().BaseHairPerTick, stateService.GetGameState().ScalingFactorUpgrades) * 60:F2}\" / second";

    public string CurrentHairLabel => $"Current hair:\n{HairLength:F2}\" / {MaxHair:F2}\"";

    public bool IsChairUnlocked => ChairState.Unlocked;

    public bool ReadyToCollect => ChairState.IsReadyToCollect(stateService.GetGameState().MaxHairUpgrades) && !ProductionTooHigh;

    public bool ProductionTooHigh => ChairState.IsProductionTooHigh(stateService.GetGameState().BaseHairPerTick, stateService.GetGameState().MaxHairUpgrades, stateService.GetGameState().ScalingFactorUpgrades);

    public bool CutHair()
    {
        if (!ReadyToCollect)
        {
            return false;
        }
        
        stateService.GetGameState().HairCollected = Math.Round(stateService.GetGameState().HairCollected + ChairState.HairLength, 3);
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