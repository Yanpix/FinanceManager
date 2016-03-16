using FinanceManager.Common;
using FinanceManager.Infrastructure;
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

        // Data service for transactions
        public IDataService<Transaction> TransactionsDataService { get; set; }

        // Data service for currencies
        public IDataService<Currency> CurrenciesDataService { get; set; }

        // Data service for categories
        public IDataService<Category> CategoriesDataService { get; set; }

        // Service for navigation
        public INavigationService NavigationService { get; set; }

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
            Transaction.MoneyBox = MoneyBox;

            TransactionsDataService.Create(Transaction);

            NavigationService.Navigate(typeof(MoneyBoxPage), new object[] { MoneyBox });
        }

        public void CancelTransaction()
        {
            //NavigationService.Navigate(typeof(MainPage));
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
                _moneyBox = (MoneyBox)NavigationService.GetNavigationData(0);
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
            Currencies = CurrenciesDataService.GetAll();
        }

        private void LoadCategories()
        {
            Categories = CategoriesDataService.GetAll()
                .Where(c => c.Type == Type)
                .OrderBy(c => c.Title)
                .ToList();
        }

        #endregion
    }
}
