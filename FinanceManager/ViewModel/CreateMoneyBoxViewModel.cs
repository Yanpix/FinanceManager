using FinanceManager.Common;
using FinanceManager.Infrastructure;
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
            // Initialize properties
            _title = "";

            // Initialize commands
            SaveMoneyBoxCommand = new RelayCommand(SaveMoneyBoxAction);
            CancelMoneyBoxCommand = new RelayCommand(CancelMoneyBoxAction);
        }

        #region Services

        // Data service for money boxes
        public IDataService<MoneyBox> MoneyBoxesDataService { get; set; }

        // Data service for currencies
        public IDataService<Currency> CurrenciesDataService { get; set; }

        // Service for navigation
        public INavigationService NavigationService { get; set; }

        #endregion

        #region Commands

        public ICommand SaveMoneyBoxCommand { get; private set; }

        public ICommand CancelMoneyBoxCommand { get; private set; }

        #endregion

        #region Commands implementation

        public void SaveMoneyBoxAction()
        {
            NavigationService.Navigate(typeof(MoneyBoxPage));
        }       

        public void CancelMoneyBoxAction()
        {
            NavigationService.Navigate(typeof(MainPage));
        }

        #endregion

        #region Properties

        private string _title;

        public string Title
        {
            get
            {
                return _title;
            }
            set
            {
                _title = value;
                OnPropertyChanged();
            }
        }

        #endregion
    }
}
