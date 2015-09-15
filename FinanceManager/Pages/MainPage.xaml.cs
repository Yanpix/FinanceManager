using Windows.UI.Xaml.Navigation;
using FinanceManager.BLL.IService;
using FinanceManager.Infrastructure;
using FinanceManager.ViewModel;

namespace FinanceManager.Pages
{
    public sealed partial class MainPage
    {
        private readonly ICurrencyService _currencyService;
        
        public MainPage(ICurrencyService currencyService)
        {
            _currencyService = currencyService;

            InitializeComponent();
            NavigationCacheMode = NavigationCacheMode.Required;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var s = _currencyService.GetAsync();
        }
    }
}
