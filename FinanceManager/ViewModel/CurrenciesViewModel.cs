using FinanceManager.Common;
using FinanceManager.Infrastructure;
using FinanceManager.Model.DataAccess.Providers;
using FinanceManager.Model.DataAccess.Services;
using FinanceManager.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FinanceManager.ViewModel
{
    public class CurrenciesViewModel : BaseViewModel
    {
        public CurrenciesViewModel()
        {
            CreateCurrencyCommand = new RelayCommand(CreateCurrency);
            DeleteAllCurrenciesCommand = new RelayCommand(DeleteAllCurrencies);
        }

        #region Services

        public INavigationService NavigationService { get; set; }

        public IDataServicesProvider DataService { get; set; }

        #endregion

        #region Commands

        public ICommand CreateCurrencyCommand { get; private set; }

        public ICommand DeleteAllCurrenciesCommand { get; private set; }

        #endregion

        #region Commands implementation

        public void CreateCurrency()
        {
            ;
        }

        public void DeleteAllCurrencies()
        {
            ;
        }

        #endregion

        #region Properties

        private List<Currency> _currencies;

        public List<Currency> Currencies
        {
            get
            {
                LoadCurrencies();
                return _currencies;
            }
            set
            {
                _currencies = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Helping methods

        private void LoadCurrencies()
        {
            _currencies = DataService.Get<Currency>().GetAll();
        }

        #endregion
    }
}
