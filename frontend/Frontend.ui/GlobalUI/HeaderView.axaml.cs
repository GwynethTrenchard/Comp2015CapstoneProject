using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Frontend.ui.Services;

namespace Frontend.ui;

public partial class HeaderView : UserControl
{
    public HeaderView()
    {
        InitializeComponent();
        if (!string.IsNullOrEmpty(UserSession.CurrentUsername))
        {
            UsernameDisplay.Text = UserSession.CurrentUsername;
        }
    }
}