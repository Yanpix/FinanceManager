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

namespace FinanceManager.ViewModel
{
    public class CreateTransactionViewModel : BaseViewModel
    {
        public CreateTransactionViewModel()
        {
            _transaction = new Transaction();

            // Initialize commands
            SaveTransactionCommand = new RelayCommand(SaveTransaction);
            CancelTransactionCommand = new RelayCommand(CancelTransaction);
        }

        #region Services

        public INavigationService NavigationService { get; set; }

        public IDataServicesProvider DataService { get; set; }

        #endregion

        #region Commands

        public ICommand SaveTransactionCommand { get; private set; }

        public ICommand CancelTransactionCommand { get; private set; }

        #endregion

        #region Commands implementation

        public void SaveTransaction()
        {
            Transaction.CreationDate = DateTime.Now;
            Transaction.Type = Type;

            DataService.Get<Transaction>().Create(Transaction);

            Transaction.MoneyBox = MoneyBox;
            DataService.Get<Transaction>().Update(Transaction);

            MoneyBox moneyBox = MoneyBox;

            if (Type == TransactionType.Income)
            {
                moneyBox.Balance =+ Transaction.Value;
            }
            else
            {
                moneyBox.Balance =- Transaction.Value;
            }

            moneyBox.LastModifiedDate = DateTime.Now;
            DataService.Get<MoneyBox>().Update(moneyBox);

            NavigationService.Navigate(typeof(MoneyBoxPage), new object[] { MoneyBox.Id });
        }

        public void CancelTransaction()
        {
            NavigationService.Navigate(typeof(MoneyBoxPage), new object[] { MoneyBox.Id });
        }

        #endregion

        #region Properties

        private TransactionType _type;

        public TransactionType Type
        {
            get
            {
                _type = (TransactionType)NavigationService.GetNavigationData(1);
                return _type;
            }
            set
            {
                _type = value;
                OnPropertyChanged();
            }
        }

        private Transaction _transaction;

        public Transaction Transaction
        {
            get
            {
                return _transaction;
            }
            set
            {
                _transaction = value;
                OnPropertyChanged();
            }
        }

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

        private List<Category> _categories;

        public List<Category> Categories
        {
            get
            {
                LoadCategories();
                return _categories;
            }
            set
            {
                _categories = value;
                OnPropertyChanged();
            }
        }

        private MoneyBox _moneyBox;

        public MoneyBox MoneyBox
        {
            get
            {
                LoadMoneyBox();
                return _moneyBox;
            }
            set
            {
                _moneyBox = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Helping methods

        private void LoadCurrencies()
        {
            _currencies = DataService.Get<Currency>().GetAll();
        }

        private void LoadCategories()
        {
            _categories = DataService.Get<Category>().GetAll()
                .Where(c => c.Type == _type)
                .OrderBy(c => c.Title)
                .ToList();
        }

        private void LoadMoneyBox()
        {
            _moneyBox = DataService.Get<MoneyBox>().Get((int)NavigationService.GetNavigationData(0));
        }

        #endregion
    }
}
