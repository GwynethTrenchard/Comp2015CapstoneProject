using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Frontend.ui;

public partial class SpellView : UserControl
{
    public SpellView()
    {
        InitializeComponent();
    }
}

// the new spell will create a new spell box like the example. i want it to sort itself via Spell level (cantrip is level 0)
// and then sort between them alphabetically by the spell name. make them easy to find and know what level they are.