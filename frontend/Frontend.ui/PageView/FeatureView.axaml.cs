using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace Frontend.ui;

public partial class FeatureView : UserControl
{
    public FeatureView()
    {
        InitializeComponent();
    }
    private void OnReturnClicked(object? sender, RoutedEventArgs e)
    {
        (TopLevel.GetTopLevel(this) as MainWindow)?.Navigate(new CharacterView());
    }
}

// the new feature will just create another box similar to the example so get that working as you will