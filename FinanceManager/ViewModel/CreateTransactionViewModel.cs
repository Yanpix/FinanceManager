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
            // Initialize commands
            SaveTransactionCommand = new RelayCommand(SaveTransaction);
            CancelTransactionCommand = new RelayCommand(CancelTransaction);
            CalculatorCommand = new RelayCommand(Calculator);
        }

        #region Services

        public INavigationService NavigationService { get; set; }

        public IDataServicesProvider DataService { get; set; }

        #endregion

        #region Commands

        public ICommand SaveTransactionCommand { get; private set; }

        public ICommand CancelTransactionCommand { get; private set; }

        public ICommand CalculatorCommand { get; private set; }

        #endregion

        #region Commands implementation

        public void SaveTransaction()
        {
            Transaction.CreationDate = DateTime.Now;
            Transaction.Type = Type;

            DataService.Get<Transaction>().Save(Transaction);

            if (Type == TransactionType.Income)
            {
                MoneyBox.Balance += Transaction.Value;
            }
            else
            {
                MoneyBox.Balance -= Transaction.Value;
            }

            MoneyBox.LastModifiedDate = DateTime.Now;
            DataService.Get<MoneyBox>().Save(MoneyBox);

            Transaction.MoneyBox = MoneyBox;
            DataService.Get<Transaction>().Update(Transaction);

            NavigationService.Navigate(typeof(MoneyBoxPage), new object[] { MoneyBox.Id });
        }

        public void CancelTransaction()
        {
            NavigationService.Navigate(typeof(MoneyBoxPage), new object[] { MoneyBox.Id });
        }

        public void Calculator()
        {
            NavigationService.Navigate(typeof(CalculatorPage));
        }

        #endregion

        #region Properties

        private TransactionType _type;

        public TransactionType Type
        {
            get
            {
                _type = LoadType();
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
                if (_transaction == null)
                    _transaction = new Transaction();
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

        private List<Category> _categories;

        public List<Category> Categories
        {
            get
            {
                if (_categories == null)
                    _categories = LoadCategories();
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

        private List<User> _users;

        public List<User> Users
        {
            get
            {
                if (_users == null)
                    _users = LoadUsers();
                return _users;
            }
            set
            {
                _users = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Helping methods

        private List<Currency> LoadCurrencies()
        {
            return DataService.Get<Currency>().GetAll();
        }

        private List<Category> LoadCategories()
        {
            return DataService.Get<Category>().GetAll()
                .Where(c => c.Type == _type)
                .OrderBy(c => c.Title)
                .ToList();
        }

        private MoneyBox LoadMoneyBox()
        {
            return DataService.Get<MoneyBox>().Get((int)NavigationService.GetNavigationData(0));
        }

        private TransactionType LoadType()
        {
            return (TransactionType)NavigationService.GetNavigationData(1);
        }

        private List<User> LoadUsers()
        {
            return DataService.Get<User>().GetAll();
        }

        #endregion
    }
}
