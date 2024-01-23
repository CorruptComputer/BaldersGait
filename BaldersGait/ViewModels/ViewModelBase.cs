using System;
using System.Reactive.Concurrency;
using ReactiveUI;

namespace BaldersGait.ViewModels;

public abstract class ViewModelBase : ReactiveObject
{
    // Roughly 60 frames per second
    private readonly TimeSpan _uiRefreshTimeSpan = TimeSpan.FromMilliseconds(16.66);
    
    protected ViewModelBase()
    {
        RxApp.MainThreadScheduler.SchedulePeriodic(_uiRefreshTimeSpan, RefreshUIFromState);
    }

    protected abstract void RefreshUIFromState();
}