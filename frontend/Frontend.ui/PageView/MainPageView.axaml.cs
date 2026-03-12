using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Frontend.ui.PageView;

namespace Frontend.ui;

public partial class MainPageView : UserControl
{
    public MainPageView()
    {
        InitializeComponent();
    }
    private void OnLogoutClicked(object? sender, RoutedEventArgs e)
    {
        (TopLevel.GetTopLevel(this) as MainWindow)?.Navigate(new LandingPageView());
    }
    private void OnCreateClicked(object? sender, RoutedEventArgs e)
    {
        (TopLevel.GetTopLevel(this) as MainWindow)?.Navigate(new CreateCampaignView());
    }
    private void OnJoinClicked(object? sender, RoutedEventArgs e)
    {
        (TopLevel.GetTopLevel(this) as MainWindow)?.Navigate(new JoinCampaignView());
    }
}