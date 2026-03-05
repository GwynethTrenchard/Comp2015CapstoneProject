using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace Frontend.ui.PageView;

public partial class RegisterPageView : UserControl
{
    public RegisterPageView()
    {
        InitializeComponent();
    }
    private void OnLoginClicked(object? sender, RoutedEventArgs e)
    {
        (TopLevel.GetTopLevel(this) as MainWindow)?.NavigateToLogin();
    }
    private void OnRegisterClicked(object? sender, RoutedEventArgs e)
    {
        //if (string.IsNullOrWhiteSpace(UsernameBox.Text) || Doesn't let you register unless theres something typed in the boxes
        //    string.IsNullOrWhiteSpace(EmailBox.Text) ||    Taken out for convincence.
        //    string.IsNullOrWhiteSpace(PasswordBox.Text))
        //{
        //    Console.WriteLine("Please fill in all fields.");
        //    return;
        //}

        (TopLevel.GetTopLevel(this) as MainWindow)?.NavigateToLogin();
    }
    private void OnBackClicked(object? sender, RoutedEventArgs e)
    {
        (TopLevel.GetTopLevel(this) as MainWindow)?.NavigateToLanding();
    }
}