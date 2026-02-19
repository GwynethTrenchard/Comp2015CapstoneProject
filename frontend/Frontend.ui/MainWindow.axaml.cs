using Avalonia.Controls;

namespace Frontend.ui;
using Frontend.ui.PageView;


public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        ContentArea.Content = new LandingPageView();
    }
}