using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace Frontend.ui;

public partial class CreateCampaignView : UserControl
{
    public CreateCampaignView()
    {
        InitializeComponent();
    }
    private void OnBackClicked(object? sender, RoutedEventArgs e)
    {
        (TopLevel.GetTopLevel(this) as MainWindow)?.Navigate(new MainPageView());
    }
}