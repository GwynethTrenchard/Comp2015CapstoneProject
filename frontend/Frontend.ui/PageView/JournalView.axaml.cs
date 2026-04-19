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
            window.Navigate(new QuestView());
        }
    }

    private void OnNoteClicked(object? sender, RoutedEventArgs e)
    {
        (TopLevel.GetTopLevel(this) as MainWindow)?.Navigate(new NotesView());
    }

    private void OnSaveNoteClicked(object sender, RoutedEventArgs e)
    {
        (TopLevel.GetTopLevel(this) as MainWindow)?.Navigate(new JournalView());
    }
}
