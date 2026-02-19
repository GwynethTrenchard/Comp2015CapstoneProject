using Avalonia.Controls;

namespace Frontend.ui;
using Frontend.ui.PageView;


public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        ContentArea.Content = new LandingPageView();
    }
    public void NavigateToLogin()
    {
        ContentArea.Content = new LoginPageView();
    }
    public void NavigateToRegister()
    {
        ContentArea.Content = new RegisterPageView();
    }
}