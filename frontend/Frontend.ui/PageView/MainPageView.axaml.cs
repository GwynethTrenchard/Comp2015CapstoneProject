using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace Frontend.ui;

public partial class MainPageView : UserControl
{
    public MainPageView()
    {
        InitializeComponent();
    }
    private void OnLogoutClicked(object? sender, RoutedEventArgs e)
    {
        (TopLevel.GetTopLevel(this) as MainWindow)?.NavigateToLanding();
    }
    private void OnCreateClicked(object? sender, RoutedEventArgs e)
    {
        (TopLevel.GetTopLevel(this) as MainWindow)?.NavigateToCreate();
    }
    private void OnJoinClicked(object? sender, RoutedEventArgs e)
    {
        (TopLevel.GetTopLevel(this) as MainWindow)?.NavigateToJoin();
    }
}