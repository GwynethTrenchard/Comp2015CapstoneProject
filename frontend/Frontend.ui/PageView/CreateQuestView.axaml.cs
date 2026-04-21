using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Frontend.ui.PageView;
using Frontend.ui.Services;

namespace Frontend.ui;

public partial class CreateQuestView : UserControl
{
    public CreateQuestView()
    {
        InitializeComponent();
    }

    private void OnReturnClicked(object sender, RoutedEventArgs e)
    {
        var window = this.VisualRoot as MainWindow;
        if (window != null)
        {
            window.Navigate(new CampaignView());
        }
    }
    private async void OnCreateClicked(object sender, RoutedEventArgs e)
    {

    }


}
