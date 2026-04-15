using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Frontend.ui.Services;
using System;
using static Frontend.ui.Services.ApiService;

namespace Frontend.ui.PageView;

public partial class LoginPageView : UserControl
{
    public LoginPageView()
    {
        InitializeComponent();
    }
    private void OnBackClicked(object? sender, RoutedEventArgs e)
    {
        (TopLevel.GetTopLevel(this) as MainWindow)?.Navigate(new LandingPageView());
    }

    private async void OnLoginClicked(object? sender, RoutedEventArgs e)
    {
        string username = UsernameBox?.Text ?? "";
        string password = PasswordBox?.Text ?? "";

        if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
        {
            System.Diagnostics.Debug.WriteLine("Please enter a username and password.");
            return;
        }

        var api = new ApiService();

        bool isSuccess = await api.LoginUser(username, password);

        if (isSuccess)
        {
            System.Diagnostics.Debug.WriteLine($"Login Success! Welcome {UserSession.CurrentUsername} (ID: {UserSession.CurrentUserId})");

            (TopLevel.GetTopLevel(this) as MainWindow)?.Navigate(new MainPageView());
        }
        else
        {
            System.Diagnostics.Debug.WriteLine("Login Failed: Invalid username or password.");
        }
    }
    private void OnRegisterClicked(object? sender, RoutedEventArgs e)
    {
        (TopLevel.GetTopLevel(this) as MainWindow)?.Navigate(new RegisterPageView());
    }
}