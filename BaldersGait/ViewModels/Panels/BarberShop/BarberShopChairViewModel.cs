using System;
using System.Linq;
using System.Reactive.Concurrency;
using BaldersGait.Models.State.BarberShop;
using BaldersGait.Services.Interface;
using ReactiveUI;

namespace BaldersGait.ViewModels.Panels.BarberShop;

public class BarberShopChairViewModel : ViewModelBase
{
    public SeatNumber SeatNumber { get; }

    public string HairLengthLabel =>
        $"{_stateService.GetBarberShopState().Seats.First(s => s.SeatNumber == SeatNumber).HairLength:F3}\"";

    public bool IsChairUnlocked =>
        _stateService.GetBarberShopState().Seats.First(s => s.SeatNumber == SeatNumber).Unlocked;
    
    private readonly IStateService _stateService;

    public BarberShopChairViewModel(IStateService stateService, SeatNumber seatNumber)
    {
        SeatNumber = seatNumber;
        _stateService = stateService;
        
        RxApp.MainThreadScheduler.SchedulePeriodic(TimeSpan.FromMilliseconds(100), RefreshUIFromState);
    }
    
    public bool CutHair()
    {
        _stateService.GetBarberShopState().HairCollected = Math.Round(_stateService.GetBarberShopState().HairCollected + _stateService.GetBarberShopState().Seats.First(s => s.SeatNumber == SeatNumber).HairLength, 3);
        _stateService.GetBarberShopState().Seats.First(s => s.SeatNumber == SeatNumber).HairLength = 0;

        return true;
    }
    
    private void RefreshUIFromState()
    {
        this.RaisePropertyChanged(nameof(HairLengthLabel));
        this.RaisePropertyChanged(nameof(IsChairUnlocked));
    }
}