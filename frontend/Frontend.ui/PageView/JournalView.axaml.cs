using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.VisualTree;
using System.Collections.Generic;
using Frontend.ui.PageView;

namespace Frontend.ui;

public partial class JournalView : UserControl
{
    private List<JournalEntry> entries = new();

    public JournalView()
    {
        InitializeComponent();
    }

    private void OnNewEntryClicked(object? sender, RoutedEventArgs e)
    {
        entries.Add(new JournalEntry());
        RefreshUI();
    }

    private void OnReturnClicked(object? sender, RoutedEventArgs e)
    {
        var window = TopLevel.GetTopLevel(this) as MainWindow;
        window?.Navigate(new LandingPageView());
    }

    private void RefreshUI()
    {
        JournalPanel.Children.Clear();

        foreach (var entry in entries)
        {
            var expander = new Expander
            {
                Header = entry.Title
            };

            var titleBox = new TextBox { Text = entry.Title };

            titleBox.GetObservable(TextBox.TextProperty).Subscribe(text =>
            {
                entry.Title = text ?? "Entry";
                RefreshUI();
            });

            var contentBox = new TextBox
            {
                AcceptsReturn = true,
                Height = 150,
                Text = entry.Content
            };

            contentBox.GetObservable(TextBox.TextProperty).Subscribe(text =>
            {
                entry.Content = text ?? "";
            });

            var panel = new StackPanel();
            panel.Children.Add(titleBox);
            panel.Children.Add(contentBox);

            expander.Content = panel;

            JournalPanel.Children.Add(expander);
        }
    }
}

class JournalEntry
{
    public string Title { get; set; } = "New Entry";
    public string Content { get; set; } = "";
}
