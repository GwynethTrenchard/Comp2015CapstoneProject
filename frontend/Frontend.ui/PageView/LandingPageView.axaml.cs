using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace Frontend.ui.PageView;

public partial class LandingPageView : UserControl
{
    public LandingPageView()
    {
        InitializeComponent();
    }
    private void OnLoginClicked(object? sender, RoutedEventArgs e)
    {
        (TopLevel.GetTopLevel(this) as MainWindow)?.NavigateToLogin();
    }

    private void OnRegisterClicked(object? sender, RoutedEventArgs e)
    {
        (TopLevel.GetTopLevel(this) as MainWindow)?.NavigateToRegister();
    }
}