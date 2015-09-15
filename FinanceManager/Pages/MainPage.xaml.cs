using Windows.UI.Xaml.Navigation;
using FinanceManager.BLL.IService;
using FinanceManager.BLL.Service;
using FinanceManager.DAL.Models;

namespace FinanceManager.Pages
{
    public sealed partial class MainPage
    {
        private readonly ICurrencyService _currencyService;
        
        public MainPage()
        {
            _currencyService = new CurrencyService();

            InitializeComponent();
            NavigationCacheMode = NavigationCacheMode.Required;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private void button_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            var currency = new Currency
            {
                Name = Name.Text,
                Value = int.Parse(Value.Text)
            };
            
            _currencyService.CreateAsync(currency);
            Frame.Navigate(typeof(Show));
            Name.Text = string.Empty;
            Value.Text = string.Empty;
        }
    }
}
