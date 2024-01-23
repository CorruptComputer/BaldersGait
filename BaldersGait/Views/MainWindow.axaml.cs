using Avalonia.Controls;

namespace BaldersGait.Views;

public partial class MainWindow : Window
{
    private const int SetHeight = 750;
    private const int SetWidth = 1400;
    
    public MainWindow()
    {
        Width = SetHeight;
        Height = SetWidth;
        CanResize = false;
        Resized += WindowBase_OnResized;
        InitializeComponent();
    }

    private void WindowBase_OnResized(object? sender, WindowResizedEventArgs e)
    {
        Height = SetHeight;
        Width = SetWidth;
    }
}