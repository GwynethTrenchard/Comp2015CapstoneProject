using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Frontend.ui.Services;
using System;

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
    private async void OnCreateCampaignClicked(object? sender, RoutedEventArgs e)
    {
        string name = CampaignNameBox?.Text ?? "";
        string desc = DescriptionBox?.Text ?? "";

        if (string.IsNullOrWhiteSpace(name))
        {
            System.Diagnostics.Debug.WriteLine("You need a campaign name!");
            return;
        }

        int currentId = UserSession.CurrentUserId ?? 0;

        if (currentId == 0)
        {
            System.Diagnostics.Debug.WriteLine("Error: No user logged in. Cannot create campaign.");
            return;
        }

        var api = new ApiService();
        bool isSuccess = await api.CreateCampaign(name, desc, currentId);

        if (isSuccess)
        {
            System.Diagnostics.Debug.WriteLine($"Success! Campaign '{name}' created by User {currentId}");

            (TopLevel.GetTopLevel(this) as MainWindow)?.Navigate(new CampaignView());
        }
        else
        {
            System.Diagnostics.Debug.WriteLine("Error: Could not save campaign to the database.");
        }
    }
}