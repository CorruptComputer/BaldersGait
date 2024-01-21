using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Concurrency;
using Avalonia.Media;
using BaldersGait.Consts;
using BaldersGait.Models.State.BarberShop;
using BaldersGait.Services.Interface;
using BaldersGait.ViewModels.Panels.BarberShop;
using ReactiveUI;

namespace BaldersGait.ViewModels.Panels;

public class BarberShopPanelViewModel : PanelBase
{
    public override string PanelName => "Barber Shop";
    public override IBrush PanelButtonBackgroundColor { get; } = Brushes.SteelBlue;

    public BarberShopTopViewModel TopView { get; init; }
    
    public BarberShopBottomViewModel BottomView { get; init; }
    
    public BarberShopPanelViewModel(IStateService stateService, BarberShopTopViewModel topView, BarberShopBottomViewModel bottomView)
    {
        TopView = topView;
        BottomView = bottomView;
    }
}