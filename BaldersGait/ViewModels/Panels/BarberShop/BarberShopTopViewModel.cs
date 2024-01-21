using System;
using System.Linq;
using System.Reactive.Concurrency;
using BaldersGait.Services.Interface;
using ReactiveUI;

namespace BaldersGait.ViewModels.Panels.BarberShop;

public class BarberShopTopViewModel : ViewModelBase
{
    public string HairCollectedLabel => $"{_stateService.GetBarberShopState().HairCollected:F3}";
    public string PurchaseClippersButtonLabel => _stateService.GetBarberShopState().ClippersPurchased ? "Clippers (purchased)" : "Purchase Clippers (500 Hair)" ;
    public string IncreaseGrowthButtonLabel => $"({_stateService.GetBarberShopState().HairGrowthUpgrades}) Increase Growth (100 Hair)";
    public string UnlockSeatButtonLabel => _stateService.GetBarberShopState().Seats.Count(s => s.Unlocked) == 8 ? "All seats unlocked" : $"Unlock seat {_stateService.GetBarberShopState().Seats.Count(s => s.Unlocked)+1} (1000 hair)";
    
    
    private readonly IStateService _stateService;
    
    public BarberShopTopViewModel(IStateService stateService)
    {
        _stateService = stateService;
        
        RxApp.MainThreadScheduler.SchedulePeriodic(TimeSpan.FromMilliseconds(100), RefreshUIFromState);
    }

    #region Click Events
    public bool PurchaseClippers_Click()
    {
        //if (HairCut < 500)
        //{
        //    return false;
        //}

        _stateService.GetBarberShopState().ClippersPurchased = true;

        return false;
    }
    
    public bool ReduceReduction_Click()
    {
        if (_stateService.GetBarberShopState().HairCollected < 2000)
        {
            return false;
        }


        return false;
    }

    public bool UnlockChair_Click()
    {
        if (_stateService.GetBarberShopState().HairCollected < 1000)
        {
            return false;
        }
        
        for (int i = _stateService.GetBarberShopState().Seats.Count(seat => seat.Unlocked); i < _stateService.GetBarberShopState().Seats.Count; i++)
        {
            if (!_stateService.GetBarberShopState().Seats[i].Unlocked)
            {
                _stateService.GetBarberShopState().HairCollected = Math.Round(_stateService.GetBarberShopState().HairCollected - 1000, 3);
                _stateService.GetBarberShopState().Seats[i].Unlocked = true;
                return true;
            }
        }

        return false;
    }
    
    public bool IncreaseGrowth_Click()
    {
        if (_stateService.GetBarberShopState().HairCollected < 100)
        {
            return false;
        }

        _stateService.GetBarberShopState().HairCollected = Math.Round(_stateService.GetBarberShopState().HairCollected - 100, 3);
        _stateService.GetBarberShopState().HairGrowthUpgrades++;
        this.RaisePropertyChanged(nameof(IncreaseGrowthButtonLabel));

        return true;
    }
    #endregion
    
    private void RefreshUIFromState()
    {
        this.RaisePropertyChanged(nameof(HairCollectedLabel));
        
        this.RaisePropertyChanged(nameof(PurchaseClippersButtonLabel));
        
        //this.RaisePropertyChanged(nameof(ReduceReductionButtonLabel));
        this.RaisePropertyChanged(nameof(UnlockSeatButtonLabel));
        this.RaisePropertyChanged(nameof(IncreaseGrowthButtonLabel));
    }
}