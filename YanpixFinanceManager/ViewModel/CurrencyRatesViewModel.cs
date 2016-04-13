using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using YanpixFinanceManager.Model.DataAccess.Services;
using YanpixFinanceManager.Model.Entities;
using YanpixFinanceManager.View;
using YanpixFinanceManager.ViewModel.Common;

namespace YanpixFinanceManager.ViewModel
{
    public class CurrencyRatesViewModel : BindableBase
    {
        #region Fields & Constructor

        private INavigationService _navigationService;

        private IEntityBaseService<Currency> _currencyService;

        private IEntityBaseService<CurrencyExchange> _currencyExchangeService;

        private ICurrencyRatesService _currencyRatesService;

        private IPlatformEvents _platformEvents;

        private Dictionary<string, object> _navigationData;

        public CurrencyRatesViewModel(INavigationService navigationService,
            IEntityBaseService<Currency> currencyService,
            IEntityBaseService<CurrencyExchange> currencyExchangeService,
            ICurrencyRatesService currencyRatesService,
            IPlatformEvents platformEvents)
        {
            _navigationService = navigationService;
            _currencyService = currencyService;
            _currencyExchangeService = currencyExchangeService;
            _currencyRatesService = currencyRatesService;
            _platformEvents = platformEvents;
            _navigationData = new Dictionary<string, object>();

            _platformEvents.BackButtonPressed += BackButtonPressed;
        }

        #endregion

        #region Commands

        private ICommand _updateCommand;

        public ICommand UpdateCommand
        {
            get
            {
                if (_updateCommand == null)
                    _updateCommand = new DelegateCommand(async (e) => await UpdateAction(e));
                return _updateCommand;
            }
            private set
            {
                _updateCommand = value;
            }
        }

        #endregion

        #region Command Actions

        private async Task UpdateAction(object e)
        {
            await _currencyRatesService.UpdateExchangeRates();
        }

        #endregion

        #region Properties

        private ObservableCollection<Currency> _fromCurrencies;

        public ObservableCollection<Currency> FromCurrencies
        {
            get
            {
                if (_fromCurrencies == null)
                    _fromCurrencies = new ObservableCollection<Currency>(_currencyService.GetAll());

                return _fromCurrencies;
            }
            set
            {
                _fromCurrencies = value;
                OnPropertyChanged();
            }
        }

        private Currency _selectedFromCurrency;

        public Currency SelectedFromCurrency
        {
            get
            {
                if (_selectedFromCurrency == null)
                    _selectedFromCurrency = FromCurrencies.First();
                return _selectedFromCurrency;
            }
            set
            {
                _selectedFromCurrency = value;
                OnPropertyChanged();
                UpdateToCurrencies();
            }
        }

        private ObservableCollection<CurrencyRate> _toCurrencies;

        public ObservableCollection<CurrencyRate> ToCurrencies
        {
            get
            {
                if (_toCurrencies == null)
                    UpdateToCurrencies();

                return _toCurrencies;
            }
            set
            {
                _toCurrencies = value;
                OnPropertyChanged();
            }
        }

        private CurrencyRate _selectedToCurrency;

        public CurrencyRate SelectedToCurrency
        {
            get
            {
                return _selectedToCurrency;
            }
            set
            {
                _selectedToCurrency = value;
                OnPropertyChanged();
                SelectedFromCurrency = FromCurrencies.Where(x => x.Id == _selectedToCurrency.Currency.Id).SingleOrDefault();
            }
        }

        private decimal _amount = 1.0M;

        public decimal Amount
        {
            get
            {
                return _amount;
            }
            set
            {
                _amount = value;
                OnPropertyChanged();
                UpdateToCurrencies();
            }
        }

        #endregion

        #region Platform Events

        private void BackButtonPressed(object sender, EventArgs e)
        {
            _navigationService.Navigate(typeof(CurrenciesPage));

            _platformEvents.BackButtonPressed -= BackButtonPressed;
        }

        #endregion

        #region Helping Methods

        private void UpdateToCurrencies()
        {
            var rates = _currencyExchangeService.GetAll();

            ToCurrencies = new ObservableCollection<CurrencyRate>(_currencyService.GetAll()
                .Where(x => x.Id != SelectedFromCurrency.Id)
                .Select(x => new CurrencyRate()
                {
                    Currency = x,
                    Value = (Amount * rates.Where(w => w.To.Equals(x.Abbreviation.ToString()) && w.From.Equals(SelectedFromCurrency.Abbreviation.ToString())).Select(s => s.Value).SingleOrDefault()).ToString("0.00")
                }));
        }

        #endregion
    }
}
