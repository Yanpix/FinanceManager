using FinanceManager.Common;
using FinanceManager.Infrastructure;
using FinanceManager.Model.DataAccess.Providers;
using FinanceManager.Model.DataAccess.Services;
using FinanceManager.Model.Entities;
using FinanceManager.Model.Entities.Enums;
using FinanceManager.View;
using FinanceManager.ViewModel.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FinanceManager.ViewModel
{
    public class CreateTransactionViewModel : BindableBase
    {
        public CreateTransactionViewModel()
        {
            navigationData = new Dictionary<string, object>();

            ValidationErrors = new ValidationErrors();

            // Initialize commands
            SaveTransactionCommand = new RelayCommand(SaveTransaction);
            CancelTransactionCommand = new RelayCommand(CancelTransaction);
            CalculatorCommand = new RelayCommand(Calculator);
        }

        Dictionary<string, object> navigationData;

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
            Validate();

            if (IsValid)
            {
                Transaction.CreationDate = DateTime.Now;
                Transaction.Type = Type;
                Transaction.Currency = SelectedCurrency;
                Transaction.Category = SelectedCategory;
                Transaction.User = SelectedUser;

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

                navigationData.Add("MoneyBoxId", MoneyBox.Id);

                NavigationService.Navigate(typeof(MoneyBoxPage), navigationData);
            }
            else
                return;
        }

        public void CancelTransaction()
        {
            navigationData.Add("MoneyBoxId", MoneyBox.Id);

            NavigationService.Navigate(typeof(MoneyBoxPage), navigationData);
        }

        public void Calculator()
        {
            navigationData.Add("SelectedCurrencyId", SelectedCurrency.Id);
            navigationData.Add("SelectedCategoryId", SelectedCategory.Id);
            navigationData.Add("SelectedUserId", SelectedUser.Id);

            NavigationService.Navigate(typeof(CalculatorPage), navigationData);
        }

        #endregion

        #region Properties

        public ValidationErrors ValidationErrors { get; set; }

        public bool IsValid { get; private set; }

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
                    _transaction = LoadTransaction();
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

        private Currency _selectedCurrency;

        public Currency SelectedCurrency
        {
            get
            {
                if (_selectedCurrency == null)
                    _selectedCurrency = LoadSelectedCurrency();
                return _selectedCurrency;
            }
            set
            {
                _selectedCurrency = value;
                OnPropertyChanged();
            }
        }

        private Category _selectedCategory;

        public Category SelectedCategory
        {
            get
            {
                if (_selectedCategory == null)
                    _selectedCategory = LoadSelectedCategory();
                return _selectedCategory;
            }
            set
            {
                _selectedCategory = value;
                OnPropertyChanged();
            }
        }

        private User _selectedUser;

        public User SelectedUser
        {
            get
            {
                if (_selectedUser == null)
                    _selectedUser = LoadSelectedUser();
                return _selectedUser;
            }
            set
            {
                _selectedUser = value;
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
            object data = NavigationService.GetNavigationData("MoneyBoxId");

            if (data != null)
                return DataService.Get<MoneyBox>().Get((int)data);
            else
                return null;
        }

        private TransactionType LoadType()
        {
            object data = NavigationService.GetNavigationData("TransactionType");

            if (data != null)
                return (TransactionType)Enum.Parse(typeof(TransactionType), data.ToString());
            else
                return TransactionType.Income;
        }

        private List<User> LoadUsers()
        {
            return DataService.Get<User>().GetAll();
        }

        private Transaction LoadTransaction()
        {
            object data = NavigationService.GetNavigationData("CalculatorResult");

            if (data != null)
            {
                Transaction transaction = new Transaction();
                transaction.Value = (decimal)data;
                return transaction;
            }
            else
                return new Transaction();
        }

        private User LoadSelectedUser()
        {
            object userId = NavigationService.GetNavigationData("SelectedUserId");

            if (userId != null)
                return Users.Where(x => x.Id == (int)userId).SingleOrDefault();
            else
                return new User();
        }

        private Category LoadSelectedCategory()
        {
            object categoryId = NavigationService.GetNavigationData("SelectedCategoryId");

            if (categoryId != null)
                return Categories.Where(x => x.Id == (int)categoryId).SingleOrDefault();
            else
                return new Category();
        }

        private Currency LoadSelectedCurrency()
        {
            object currencyId = NavigationService.GetNavigationData("SelectedCurrencyId");

            if (currencyId != null)
                return Currencies.Where(x => x.Id == (int)currencyId).SingleOrDefault();
            else
                return new Currency();
        }

        private void Validate()
        {
            ValidationErrors.Clear();

            if (Transaction.Value <= 0)
            {
                ValidationErrors["Value"] = "value must be positive";
            }

            if (string.IsNullOrEmpty(SelectedCurrency.Title))
            {
                ValidationErrors["Currency"] = "currency is required";
            }

            if (string.IsNullOrEmpty(SelectedCategory.Title))
            {
                ValidationErrors["Category"] = "category is required";
            }

            if (string.IsNullOrEmpty(SelectedUser.Title))
            {
                ValidationErrors["User"] = "user is required";
            }

            IsValid = ValidationErrors.IsValid;

            OnPropertyChanged("IsValid");
            OnPropertyChanged("ValidationErrors");
        }

        #endregion
    }
}
