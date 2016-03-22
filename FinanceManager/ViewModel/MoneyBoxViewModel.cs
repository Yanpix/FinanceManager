using FinanceManager.Common;
using FinanceManager.Infrastructure;
using FinanceManager.Model.DataAccess.Providers;
using FinanceManager.Model.DataAccess.Services;
using FinanceManager.Model.Entities;
using FinanceManager.Model.Entities.Enums;
using FinanceManager.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml;

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

        public INavigationService NavigationService { get; set; }

        public IDataServicesProvider DataService { get; set; }

        #endregion

        #region Commands

        public ICommand CreateIncomeCommand { get; private set; }

        public ICommand CreateExpenceCommand { get; private set; }

        #endregion

        #region Commands implementation

        private void CreateIncome()
        {
            NavigationService.Navigate(typeof(CreateTransactionPage), 
                new object[] { MoneyBox.Id, TransactionType.Income });
        }

        private void CreateExpence()
        {
            NavigationService.Navigate(typeof(CreateTransactionPage), 
                new object[] { MoneyBox.Id, TransactionType.Expence });
        }

        #endregion

        #region Properties

        private MoneyBox _moneyBox;

        public MoneyBox MoneyBox
        {
            get
            {
                if (_moneyBox == null)
                    _moneyBox = LoadMoneyBox();
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
                if (_lastTransaction == null)
                    _lastTransaction = LoadLastTransaction();
                return _lastTransaction;
            }
            set
            {
                _lastTransaction = value;
                OnPropertyChanged();
            }
        }

        private Transaction _previousTransaction;

        public Transaction PreviousTransaction
        {
            get
            {
                if (_previousTransaction == null)
                    _previousTransaction = LoadPreviousTransaction();
                return _previousTransaction;
            }
            set
            {
                _previousTransaction = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Helping methods

        private Transaction LoadLastTransaction()
        {
            return DataService.Get<Transaction>()
                .GetAll()
                .Where(t => t.MoneyBoxId == MoneyBox.Id)
                .OrderByDescending(o => o.CreationDate)
                .FirstOrDefault();
        }

        private Transaction LoadPreviousTransaction()
        {
            return DataService.Get<Transaction>()
                .GetAll()
                .Where(t => t.MoneyBoxId == MoneyBox.Id)
                .OrderByDescending(o => o.CreationDate)
                .Skip(1)
                .FirstOrDefault();
        }

        private MoneyBox LoadMoneyBox()
        {
            return DataService.Get<MoneyBox>().Get((int)NavigationService.GetNavigationData(0));
        }

        #endregion
    }
}
