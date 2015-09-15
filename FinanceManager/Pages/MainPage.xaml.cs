using Windows.UI.Xaml.Navigation;
using FinanceManager.BLL.IService;
using FinanceManager.BLL.Service;
using static System.String;

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
            _currencyService.CreateAsync(textBox.Text);
            Frame.Navigate(typeof(Show));
            textBox.Text = Empty;
        }
    }
}
