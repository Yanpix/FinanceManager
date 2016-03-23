using FinanceManager.Common;
using FinanceManager.Infrastructure;
using FinanceManager.Model.DataAccess.Providers;
using FinanceManager.Model.Entities;
using FinanceManager.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FinanceManager.ViewModel
{
    public class CreateCurrencyViewModel : BaseViewModel
    {
        public CreateCurrencyViewModel()
        {
            SaveCurrencyCommand = new RelayCommand(SaveCurrency);
            CancelCurrencyCommand = new RelayCommand(CancelCurrency);
        }

        #region Services

        public INavigationService NavigationService { get; set; }

        public IDataServicesProvider DataService { get; set; }

        #endregion

        #region Commands

        public ICommand SaveCurrencyCommand { get; private set; }

        public ICommand CancelCurrencyCommand { get; private set; }

        #endregion

        #region Commands implementation

        public void SaveCurrency()
        {
            DataService.Get<Currency>().Save(Currency);

            NavigationService.Navigate(typeof(MainPage));
        }

        public void CancelCurrency()
        {
            NavigationService.Navigate(typeof(MainPage));
        }

        #endregion

        #region Properties

        private Currency _currency;

        public Currency Currency
        {
            get
            {
                if (_currency == null)
                    _currency = new Currency();
                return _currency;
            }
            set
            {
                _currency = value;
                OnPropertyChanged();
            }
        }

        #endregion
    }
}
