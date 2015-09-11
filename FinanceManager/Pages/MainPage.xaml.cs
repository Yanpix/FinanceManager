using Windows.UI.Xaml.Navigation;
using FinanceManager.BLL.Service;

namespace FinanceManager.Pages
{
    public sealed partial class MainPage
    {
        private readonly CurrencyService _currencyService;
        public MainPage()
        {
            _currencyService = new CurrencyService();

            InitializeComponent();
            NavigationCacheMode = NavigationCacheMode.Required;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {

        }
    }
}
