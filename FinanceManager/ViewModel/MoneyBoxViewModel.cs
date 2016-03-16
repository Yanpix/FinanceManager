using FinanceManager.Common;
using FinanceManager.Infrastructure;
using FinanceManager.Model.Entities;
using FinanceManager.Model.Entities.Enums;
using FinanceManager.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FinanceManager.ViewModel
{
    public class MoneyBoxViewModel : BaseViewModel
    {
        public MoneyBoxViewModel()
        {


            CreateIncomeCommand = new RelayCommand(CreateIncome);
            CreateExpenceCommand = new RelayCommand(CreateExpence);
        }

        #region Services

        // Service for navigation
        public INavigationService NavigationService { get; set; }

        #endregion

        #region Commands

        public ICommand CreateIncomeCommand { get; private set; }

        public ICommand CreateExpenceCommand { get; private set; }

        #endregion

        #region Commands implementation

        private void CreateIncome()
        {
            NavigationService.Navigate(typeof(CreateTransactionPage), 
                new object[] { MoneyBox, TransactionType.Income });
        }

        private void CreateExpence()
        {
            NavigationService.Navigate(typeof(CreateTransactionPage), 
                new object[] { MoneyBox, TransactionType.Expence });
        }

        #endregion

        #region Properties

        private MoneyBox _moneyBox;

        public MoneyBox MoneyBox
        {
            get
            {
                _moneyBox = (MoneyBox)NavigationService.GetNavigationData(0);
                return _moneyBox;
            }
            set
            {
                _moneyBox = value;
                OnPropertyChanged();
            }
        }

        private Transaction _lastTransaction;

        public Transaction LastTransaction
        {
            get
            {
                LoadLastTransaction();
                return _lastTransaction;
            }
            set
            {
                _lastTransaction = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Helping methods

        private void LoadLastTransaction()
        {
            if (MoneyBox.Transactions.Count != 0)
            {
                _lastTransaction = MoneyBox.Transactions.Last();
            }            
        }

        #endregion
    }
}
