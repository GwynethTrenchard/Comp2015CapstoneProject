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

    public void Navigate (UserControl newPage)
    {
        ContentArea.Content = newPage;
    }
    //public void NavigateToLogin()
    //{
    //    ContentArea.Content = new LoginPageView();
    //}
    //public void NavigateToRegister()
    //{
    //    ContentArea.Content = new RegisterPageView();
    //}
    //public void NavigateToLanding()
    //{
    //    ContentArea.Content = new LandingPageView();
    //}
    //public void NavigateToMain()
    //{
    //    ContentArea.Content = new MainPageView();
    //}
    ////public void NavigateToCreate()
    ////{
    ////    ContentArea.Content = new CreateCampaignView();
    ////}
    //public void NavigateToJoin()
    //{
    //    ContentArea.Content = new JoinCampaignView();
    //}
}