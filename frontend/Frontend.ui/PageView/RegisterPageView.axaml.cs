using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Frontend.ui.Services;
using System;
using Tmds.DBus.Protocol;

namespace Frontend.ui.PageView;

public partial class RegisterPageView : UserControl
{
    public RegisterPageView()
    {
        InitializeComponent();
    }
    private void OnLoginClicked(object? sender, RoutedEventArgs e)
    {
        (TopLevel.GetTopLevel(this) as MainWindow)?.Navigate(new LoginPageView());
    }
    private async void OnRegisterClicked(object? sender, RoutedEventArgs e)
    {

        string user = UsernameBox?.Text ?? "";
        string email = EmailBox?.Text ?? "";
        string pass = PasswordBox?.Text ?? "";
        string confirm = ConfirmPasswordBox?.Text ?? "";

        if (string.IsNullOrWhiteSpace(user) || string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(pass))
        {
            Console.WriteLine("All fields are required!");
            return;
        }

        if (pass != confirm)
        {
            Console.WriteLine("Passwords do not match!");
            return;
        }

        var api = new ApiService();
        bool isSuccess = await api.RegisterUser(user, email, pass);

        if (isSuccess)
        {
            (TopLevel.GetTopLevel(this) as MainWindow)?.Navigate(new LoginPageView());
        }
    }
    private void OnBackClicked(object? sender, RoutedEventArgs e)
    {
        (TopLevel.GetTopLevel(this) as MainWindow)?.Navigate(new LandingPageView());
    }
}