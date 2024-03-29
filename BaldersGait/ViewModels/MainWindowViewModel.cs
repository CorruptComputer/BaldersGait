﻿using BaldersGait.Models.Enums;
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

            this.RaiseAndSetIfChanged(ref _currentPanel, value);
        }
    }

    public MainWindowViewModel(SidebarViewModel sidebar)
    {
        if (Current != null)
        {
            const string message = $"More than one {nameof(MainWindowViewModel)} has been created";
            Log.Error(message);
            throw new BaldersGaitException(message, false);
        }

        Current = this;
        _sidebar = sidebar;

        SidebarButtonViewModel button = _sidebar.Buttons.First(x => x.ButtonType == ButtonTypes.Panel);

        _currentPanel = button?.PanelToOpen ?? throw new BaldersGaitException("Unable to find starting panel.", false);
        Log.Information($"Starting panel: {_currentPanel.PanelName}");
    }

    protected override void RefreshUIFromState()
    {
        // Nothing to do
    }
}