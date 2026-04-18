using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Frontend.ui.PageView;

namespace Frontend.ui;

public partial class DMNotesView : UserControl
{
    public DMNotesView()
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
}

