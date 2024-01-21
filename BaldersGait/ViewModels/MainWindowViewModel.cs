using System;
using System.Linq;
using System.Reactive.Concurrency;
using System.Threading.Tasks;
using BaldersGait.Services.Interface;
using BaldersGait.ViewModels.Panels;
using BaldersGait.ViewModels.Sidebar;
using ReactiveUI;
using Serilog;

namespace BaldersGait.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    public static MainWindowViewModel? Current { get; private set; }

    private SidebarViewModel _sidebar;
    public SidebarViewModel Sidebar
    {
        get => _sidebar;
        set => this.RaiseAndSetIfChanged(ref _sidebar, value);
    }

    private PanelBase _currentPanel;
    public PanelBase CurrentPanel
    {
        get => _currentPanel;
        set
        {
            if (value.PanelName == _currentPanel.PanelName)
            {
                return;
            }

            Parallel.ForEach(_sidebar.Buttons, sidebarButton =>
            {
                if (sidebarButton.ButtonText != value.PanelName)
                {
                    sidebarButton.ResetBackground();
                }
                else
                {
                    sidebarButton.SetBackgroundSelected();
                }
            });

            this.RaiseAndSetIfChanged(ref _currentPanel, value);
        }
    }
    
    public MainWindowViewModel(SidebarViewModel sidebar, IStateService stateService)
    {
        if (Current != null)
        {
            Log.Error($"More than one {nameof(MainWindowViewModel)} has been created");
            throw new();
        }

        Current = this;
        _sidebar = sidebar;

        SidebarButtonViewModel button = _sidebar.Buttons.First();
        _currentPanel = button.PanelToOpen;
        Log.Information($"Starting panel: {_currentPanel.PanelName}");
        button.SetBackgroundSelected();
        
        RxApp.TaskpoolScheduler.SchedulePeriodic(TimeSpan.FromMilliseconds(100), stateService.TickState);
    }
}