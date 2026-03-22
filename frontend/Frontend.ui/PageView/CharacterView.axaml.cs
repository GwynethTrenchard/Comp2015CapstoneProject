using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Frontend.ui;

public partial class CharacterView : UserControl
{
    public CharacterView()
    {
        InitializeComponent();
    }
}


//calculation for the Ability scores modifier (x = ability score) is y = (x/2)-5 floor
//calculation for the proficiency bonus (x is character level) is y = 1+(x/4+1) rounded down