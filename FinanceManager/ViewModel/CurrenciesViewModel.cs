using FinanceManager.Common;
using FinanceManager.Infrastructure;
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
            _currencies = new List<Currency>();

            CreateCurrencyCommand = new RelayCommand(CreateCurrency);
            DeleteAllCurrenciesCommand = new RelayCommand(DeleteAllCurrencies);
        }

        #region Services

        // Data service for currencies
        public IDataService<Currency> CurrenciesDataService { get; set; }

        // Service for navigation
        public INavigationService NavigationService { get; set; }

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
            Currencies = CurrenciesDataService.GetAll();
        }

        #endregion
    }
}
