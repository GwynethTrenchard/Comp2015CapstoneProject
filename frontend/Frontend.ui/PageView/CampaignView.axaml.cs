using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Frontend.ui.Services;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Frontend.ui;

public partial class CampaignView : UserControl
{
private readonly ApiService _api = new ApiService();
    public CampaignView()
    {
        InitializeComponent();
        LoadCampaignDetails();
    }

    private void UpdateUI(Campaign campaign)
    {
        CampaignTitleText.Text = campaign.campaign_name;
        CampaignDescriptionText.Text = campaign.description;
        UserSession.ActiveCampaignId = campaign.campaign_id;
    }

    private async void LoadCampaignDetails()
    {
        int? targetId = UserSession.ActiveCampaignId;
        var allCampaigns = await _api.GetUserCampaigns();

        if (targetId != null)
        {
            var selected = allCampaigns.FirstOrDefault(c => c.campaign_id == targetId);
            if (selected != null)
            {
                UpdateUI(selected);
            }
        }
        else
        {
            var latest = allCampaigns.OrderByDescending(c => c.campaign_id).FirstOrDefault();
            if (latest != null)
            {
                UpdateUI(latest);
            }
        }
    }

    private void OnQuestClicked(object? sender, RoutedEventArgs e)
    {
        (TopLevel.GetTopLevel(this) as MainWindow)?.Navigate(new QuestView());
    }

    private void OnCreateQuestClicked(object? sender, RoutedEventArgs e)
    {
        (TopLevel.GetTopLevel(this) as MainWindow)?.Navigate(new CreateQuestView());
    }

    private void OnReturnClicked(object? sender, RoutedEventArgs e)
    {
        (TopLevel.GetTopLevel(this) as MainWindow)?.Navigate(new MainPageView());
    }
    private void OnCharacterClicked(object? sender, RoutedEventArgs e)
    {
        (TopLevel.GetTopLevel(this) as MainWindow)?.Navigate(new CharacterView());
    }

    private void OnCreateCharacterClicked(object? sender, RoutedEventArgs e)
    {
        (TopLevel.GetTopLevel(this) as MainWindow)?.Navigate(new CreateCharacterView());
    }


}