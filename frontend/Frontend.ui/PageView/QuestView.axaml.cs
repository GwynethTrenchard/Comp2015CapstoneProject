using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.VisualTree;
using System.Collections.Generic;
using System.Linq;
using Frontend.ui.PageView;

namespace Frontend.ui;

public partial class QuestView : UserControl
{
    private List<Quest> quests = new();

    public QuestView()
    {
        InitializeComponent();
    }

    private void OnNewQuestClicked(object? sender, RoutedEventArgs e)
    {
        quests.Add(new Quest());
        RefreshUI();
    }

    private void OnReturnClicked(object? sender, RoutedEventArgs e)
    {
        var window = TopLevel.GetTopLevel(this) as MainWindow;
        window?.Navigate(new LandingPageView());
    }

    private void RefreshUI()
    {
        QuestListPanel.Children.Clear();

        var sorted = quests
        .OrderBy(q => q.Status)
        .ThenBy(q => q.Name);

        foreach (var quest in sorted)
        {
            var expander = new Expander
            {
                Header = $"{quest.Name} [{quest.Status}]"
            };

            var nameBox = new TextBox { Text = quest.Name };

            nameBox.GetObservable(TextBox.TextProperty).Subscribe(text =>
            {
                quest.Name = text ?? "Unnamed";
                RefreshUI();
            });

            var statusBox = new ComboBox
            {
                Items = new List<string> { "Active", "Completed", "Failed" },
                SelectedItem = quest.Status
            };

            statusBox.SelectionChanged += (_, __) =>
            {
                quest.Status = statusBox.SelectedItem?.ToString() ?? "Active";
                RefreshUI();
            };

            var panel = new StackPanel();
            panel.Children.Add(nameBox);
            panel.Children.Add(statusBox);

            expander.Content = panel;

            QuestListPanel.Children.Add(expander);
        }
    }
}

class Quest
{
    public string Name { get; set; } = "New Quest";
    public string Status { get; set; } = "Active";
}

