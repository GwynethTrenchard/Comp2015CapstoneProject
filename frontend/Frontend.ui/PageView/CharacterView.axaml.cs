using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace Frontend.ui;

public partial class CharacterView : UserControl
{
    public CharacterView()
    {
        InitializeComponent();
    }

    private void OnReturnClicked(object? sender, RoutedEventArgs e)
    {
        (TopLevel.GetTopLevel(this) as MainWindow)?.Navigate(new CampaignView());
    }

    private void OnFeaturesClicked(object? sender, RoutedEventArgs e)
    {
        (TopLevel.GetTopLevel(this) as MainWindow)?.Navigate(new FeatureView());
    }

    private void OnSpellsClicked(object? sender, RoutedEventArgs e)
    {
        (TopLevel.GetTopLevel(this) as MainWindow)?.Navigate(new SpellView());
    }
}


//calculation for the Ability scores modifier (x = ability score) is y = (x/2)-5 floor
//calculation for the proficiency bonus (x is character level) is y = 1+(x/4+1) rounded down