using Windows.UI.Xaml.Navigation;
using FinanceManager.BLL.IService;
using FinanceManager.BLL.Service;
using FinanceManager.DAL.Models;

namespace FinanceManager.Pages
{
    public sealed partial class MainPage
    {
       
        public MainPage()
        {
            InitializeComponent();
            NavigationCacheMode = NavigationCacheMode.Required;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private void BtnCategories_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            Frame.Navigate(typeof (Categories));
        }

        private void BtnExpenseCategory_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            Frame.Navigate(typeof (EditExpenseCategory));
        }

        private void BtnIncomeCategory_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            Frame.Navigate(typeof (EditIncomeCategory));
        }

        private void BtnCurrencies_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            Frame.Navigate(typeof (Currencies));
        }
    }
}
