using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Frontend.ui.PageView;

namespace Frontend.ui;

public partial class QuestView : UserControl
{
    public QuestView()
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

    private void OnJournalClicked(object? sender, RoutedEventArgs e)
    {
        (TopLevel.GetTopLevel(this) as MainWindow)?.Navigate(new JournalView());
    }

    private void OnNoteClicked(object? sender, RoutedEventArgs e)
    {
        (TopLevel.GetTopLevel(this) as MainWindow)?.Navigate(new NotesView());
    }

    private void OnCreateNoteClicked(object? sender, RoutedEventArgs e)
    {
        (TopLevel.GetTopLevel(this) as MainWindow)?.Navigate(new NotesView());
    }
}
