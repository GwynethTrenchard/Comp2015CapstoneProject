using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace Frontend.ui.PageView;

public partial class LoginPageView : UserControl
{
    public LoginPageView()
    {
        InitializeComponent();
    }
    private void OnBackClicked(object? sender, RoutedEventArgs e)
    {
        (TopLevel.GetTopLevel(this) as MainWindow)?.NavigateToLanding();
    }
    private void OnLoginClicked(object? sender, RoutedEventArgs e)
    {
        (TopLevel.GetTopLevel(this) as MainWindow)?.NavigateToMain();
    }
    private void OnRegisterClicked(object? sender, RoutedEventArgs e)
    {
        (TopLevel.GetTopLevel(this) as MainWindow)?.NavigateToRegister();
    }
}