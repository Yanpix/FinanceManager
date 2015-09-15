using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using FinanceManager.BLL.Service;
using FinanceManager.DAL.Models;

namespace FinanceManager.Pages
{
    public sealed partial class UpdateOrDelete
    {
        int _selectedContactId;
        private readonly CurrencyService _currencyService;

        public UpdateOrDelete()
        {
            _currencyService = new CurrencyService();
            InitializeComponent();
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            _selectedContactId = int.Parse(e.Parameter.ToString());
            var currency = await _currencyService.GetByIdAsync(_selectedContactId);
            NameTxbx.Text = currency.Name;
            ValueTxbx.Text = currency.Value.ToString();
        }

        private async void UpdateContact_Click(object sender, RoutedEventArgs e)
        {
            var currency = new Currency
            {
                Id = _selectedContactId,
                Name = NameTxbx.Text,
                Value = int.Parse(ValueTxbx.Text)
            };

            await _currencyService.UpdateAsync(currency);
            Frame.Navigate(typeof(Show));
        }
        private async void DeleteContact_Click(object sender, RoutedEventArgs e)
        {
            await _currencyService.DeleteAsync(_selectedContactId);
            Frame.Navigate(typeof(Show));
        }
    }
}