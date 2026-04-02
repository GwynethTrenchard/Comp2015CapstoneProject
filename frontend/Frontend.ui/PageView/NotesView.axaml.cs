
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.VisualTree;
using System.Collections.Generic;
using Frontend.ui.PageView;

namespace Frontend.ui;

public partial class NotesView : UserControl
{
    private List<Note> notes = new();

    public NotesView()
    {
        InitializeComponent();
    }

    private void OnNewNoteClicked(object? sender, RoutedEventArgs e)
    {
        notes.Add(new Note());
        RefreshUI();
    }

    private void OnReturnClicked(object? sender, RoutedEventArgs e)
    {
        var window = TopLevel.GetTopLevel(this) as MainWindow;
        window?.Navigate(new LandingPageView());
    }

    private void RefreshUI()
    {
        NotesPanel.Children.Clear();

        foreach (var note in notes)
        {
            var box = new TextBox
            {
                AcceptsReturn = true,
                Height = 120,
                Text = note.Content
            };

            box.GetObservable(TextBox.TextProperty).Subscribe(text =>
            {
                note.Content = text ?? "";
            });

            NotesPanel.Children.Add(box);
        }
    }
}