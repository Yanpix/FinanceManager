using FinanceManager.Common;
using FinanceManager.Infrastructure;
using FinanceManager.Model.DataAccess.Providers;
using FinanceManager.Model.DataAccess.Services;
using FinanceManager.Model.Entities;
using FinanceManager.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace FinanceManager.ViewModel
{
    /// <summary>
    /// View model for CreateMoneyBoxPage, implements BaseViewModel
    /// </summary>
    public class CreateMoneyBoxViewModel : BaseViewModel
    {
        // View model constructor
        public CreateMoneyBoxViewModel()
        {
            // Initialize commands
            SaveMoneyBoxCommand = new RelayCommand(SaveMoneyBox);
            CancelMoneyBoxCommand = new RelayCommand(CancelMoneyBox);
        }

        #region Services

        public INavigationService NavigationService { get; set; }

        public IDataServicesProvider DataService { get; set; }

        #endregion

        #region Commands

        public ICommand SaveMoneyBoxCommand { get; private set; }

        public ICommand CancelMoneyBoxCommand { get; private set; }

        #endregion

        #region Commands implementation

        public void SaveMoneyBox()
        {
            MoneyBox.CreationDate = DateTime.Now;
            MoneyBox.LastModifiedDate = DateTime.Now;
            MoneyBox.Balance = 0.0M;

            DataService.Get<MoneyBox>().Save(MoneyBox);

            NavigationService.Navigate(typeof(MoneyBoxPage), new object[] { MoneyBox.Id });
        }       

        public void CancelMoneyBox()
        {
            NavigationService.Navigate(typeof(MainPage));
        }

        #endregion

        #region Properties

        private MoneyBox _moneyBox;

        public MoneyBox MoneyBox
        {
            get
            {
                if (_moneyBox == null)
                    _moneyBox = new MoneyBox();
                return _moneyBox;
            }
            set
            {
                _moneyBox = value;
                OnPropertyChanged();
            }
        }

        private List<Currency> _currencies;

        public List<Currency> Currencies
        {
            get
            {
                if (_currencies == null)
                    _currencies = LoadCurrencies();
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

        private List<Currency> LoadCurrencies()
        {
            return DataService.Get<Currency>().GetAll();
        }

        #endregion
    }
}
