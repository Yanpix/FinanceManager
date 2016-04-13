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
    public class CurrenciesViewModel : BindableBase
    {
        #region Fields & Constructor

        private INavigationService _navigationService;

        private IEntityBaseService<Currency> _currencyService;

        private IPlatformEvents _platformEvents;

        private Dictionary<string, object> _navigationData;

        public CurrenciesViewModel(INavigationService navigationService,
            IEntityBaseService<Currency> currencyService,
            IPlatformEvents platformEvents)
        {
            _navigationService = navigationService;
            _currencyService = currencyService;
            _platformEvents = platformEvents;
            _navigationData = new Dictionary<string, object>();

            _platformEvents.BackButtonPressed += BackButtonPressed;
        }

        #endregion

        #region Commands

        private ICommand _ratesCommand;

        public ICommand RatesCommand
        {
            get
            {
                if (_ratesCommand == null)
                    _ratesCommand = new DelegateCommand((e) => RatesAction(e));
                return _ratesCommand;
            }
            private set
            {
                _ratesCommand = value;
            }
        }

        #endregion

        #region Command Actions

        private void RatesAction(object e)
        {
            _navigationService.Navigate(typeof(CurrencyRatesPage));
        }

        #endregion

        #region Properties

        private ObservableCollection<Currency> _currencies;

        public ObservableCollection<Currency> Currencies
        {
            get
            {
                if (_currencies == null)
                    _currencies = new ObservableCollection<Currency>(_currencyService.GetAll());

                return _currencies;
            }
            set
            {
                _currencies = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Platform Events

        private void BackButtonPressed(object sender, EventArgs e)
        {
            _navigationService.Navigate(typeof(MainPage));

            _platformEvents.BackButtonPressed -= BackButtonPressed;
        }

        #endregion
    }
}
