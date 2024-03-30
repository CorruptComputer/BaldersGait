using System.Reactive.Concurrency;
using ReactiveUI;

namespace BaldersGait.ViewModels;

public abstract class ViewModelBase : ReactiveObject
{
    // Roughly 60 times per second
    private readonly TimeSpan _uiRefreshTimeSpan = TimeSpan.FromMilliseconds(16.66);

    protected ViewModelBase(bool autoRefreshUi)
    {
        if (autoRefreshUi)
        {
            RxApp.MainThreadScheduler.SchedulePeriodic(_uiRefreshTimeSpan, RefreshUIFromState);
        }
    }

    protected abstract void RefreshUIFromState();
}