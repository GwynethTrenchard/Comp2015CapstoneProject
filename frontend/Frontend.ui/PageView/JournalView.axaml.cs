using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Frontend.ui.PageView;

namespace Frontend.ui;

public partial class JournalView : UserControl
{
    public JournalView()
    {
        InitializeComponent();
    }

    private void OnReturnClicked(object sender, RoutedEventArgs e)
    {
        var window = this.VisualRoot as MainWindow;
        if (window != null)
        {
            window.Navigate(new LandingPageView());
        }
    }

    private void OnAddNoteClicked(object sender, RoutedEventArgs e)
    {
        // Placeholder for future logic
    }
}
