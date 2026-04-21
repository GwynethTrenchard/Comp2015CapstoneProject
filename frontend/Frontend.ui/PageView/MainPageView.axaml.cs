using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Frontend.ui.PageView;
using Frontend.ui.Services;
using System.Collections.Generic;
using static Frontend.ui.Services.ApiService;

namespace Frontend.ui;

public partial class MainPageView : UserControl
{
    private List<Campaign> _myCampaigns = new();
    private int _currentIndex = 0;
    private readonly ApiService _api = new ApiService();
    public MainPageView()
    {
        InitializeComponent();
        LoadGames();
    }
    private async void LoadGames()
    {
        _myCampaigns = await _api.GetUserCampaigns();
        UpdateCampaignDisplay();
    }

    private void UpdateCampaignDisplay()
    {
        if (_myCampaigns.Count > 0)
        {
            CreatedCampaignTitle.Text = _myCampaigns[_currentIndex].campaign_name;
        }
        else
        {
            CreatedCampaignTitle.Text = "No Games Created";
        }
    }

    private void OnNextGameClicked(object? sender, RoutedEventArgs e)
    {
        if (_myCampaigns.Count == 0) return;

        _currentIndex = (_currentIndex + 1) % _myCampaigns.Count;
        UpdateCampaignDisplay();
    }

    private void OnPrevGameClicked(object? sender, RoutedEventArgs e)
    {
        if (_myCampaigns.Count == 0) return;

        _currentIndex = (_currentIndex - 1 + _myCampaigns.Count) % _myCampaigns.Count;
        UpdateCampaignDisplay();
    }

    private void OnJoinClicked(object? sender, RoutedEventArgs e)
    {
        if (_myCampaigns != null && _myCampaigns.Count > 0)
        {
            var selectedCampaign = _myCampaigns[_currentIndex];

            UserSession.ActiveCampaignId = selectedCampaign.campaign_id;

            System.Diagnostics.Debug.WriteLine($"Joining Campaign: {selectedCampaign.campaign_name} (ID: {selectedCampaign.campaign_id})");

            (TopLevel.GetTopLevel(this) as MainWindow)?.Navigate(new CampaignView());
        }
        else
        {
            System.Diagnostics.Debug.WriteLine("No campaign selected to join.");
        }
    }
    private void OnLogoutClicked(object? sender, RoutedEventArgs e)
    {
        UserSession.CurrentUserId = null;
        UserSession.ActiveCampaignId = null;

        System.Diagnostics.Debug.WriteLine("User logged out. Session cleared.");
        (TopLevel.GetTopLevel(this) as MainWindow)?.Navigate(new LandingPageView());
    }
    private void OnCreateClicked(object? sender, RoutedEventArgs e)
    {
        (TopLevel.GetTopLevel(this) as MainWindow)?.Navigate(new CreateCampaignView());
    }
    private void OnJoinCampaignClicked(object? sender, RoutedEventArgs e)
    {
        (TopLevel.GetTopLevel(this) as MainWindow)?.Navigate(new JoinCampaignView());
    }
}