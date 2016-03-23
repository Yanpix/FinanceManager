using FinanceManager.Common;
using FinanceManager.Infrastructure;
using FinanceManager.Model.DataAccess.Providers;
using FinanceManager.Model.DataAccess.Services;
using FinanceManager.Model.Entities;
using FinanceManager.Model.Entities.Enums;
using FinanceManager.View;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using System;

namespace FinanceManager.ViewModel
{
    /// <summary>
    /// View model for MainPage, implements BaseViewModel
    /// </summary>
    public class MainViewModel : BaseViewModel
    {
        // View model constructor
        public MainViewModel()
        {
            // Initialize commands
            CreateMoneyBoxCommand = new RelayCommand(CreateMoneyBox);
            DeleteAllMoneyBoxesCommand = new RelayCommand(DeleteAllMoneyBoxes);

            CreateUserCommand = new RelayCommand(CreateUser);
            DeleteAllUsersCommand = new RelayCommand(DeleteAllUsers);

            CreateCategoryCommand = new RelayCommand(CreateCategory);
            DeleteAllCategoriesCommand = new RelayCommand(DeleteAllCategories);

            CreateCurrencyCommand = new RelayCommand(CreateCurrency);
            DeleteAllCurrenciesCommand = new RelayCommand(DeleteAllCurrencies);           
        }

        #region Services

        public INavigationService NavigationService { get; set; }

        public IDataServicesProvider DataService { get; set; }

        #endregion

        #region Commands

        public ICommand CreateMoneyBoxCommand { get; private set; }

        public ICommand DeleteAllMoneyBoxesCommand { get; private set; }

        public ICommand CreateUserCommand { get; private set; }

        public ICommand DeleteAllUsersCommand { get; private set; }

        public ICommand CreateCategoryCommand { get; private set; }

        public ICommand DeleteAllCategoriesCommand { get; private set; }

        public ICommand CreateCurrencyCommand { get; private set; }

        public ICommand DeleteAllCurrenciesCommand { get; private set; }

        #endregion

        #region Commands implementation

        public void CreateMoneyBox()
        {
            NavigationService.Navigate(typeof(CreateMoneyBoxPage)); 
        }   

        private void DeleteAllMoneyBoxes()
        {
            DataService.Get<MoneyBox>().DeleteAll();
            MoneyBoxes = LoadMoneyBoxes();
        }

        public void CreateUser()
        {
            NavigationService.Navigate(typeof(CreateUserPage));
        }

        private void DeleteAllUsers()
        {
            DataService.Get<User>().DeleteAll();
            Users = LoadUsers();
        }

        public void CreateCategory()
        {
            NavigationService.Navigate(typeof(CreateCategoryPage));
        }

        private void DeleteAllCategories()
        {
            DataService.Get<Category>().DeleteAll();
            IncomeCategories = LoadIncomeCategories();
            ExpenceCategories = LoadExpenceCategories();
        }

        public void CreateCurrency()
        {
            NavigationService.Navigate(typeof(CreateCurrencyPage));
        }

        private void DeleteAllCurrencies()
        {
            DataService.Get<Currency>().DeleteAll();
            Currencies = LoadCurrencies();
        }

        #endregion

        #region Properties

        private List<MoneyBox> _moneyBoxes;

        public List<MoneyBox> MoneyBoxes
        {
            get
            {
                if (_moneyBoxes == null)
                    _moneyBoxes = LoadMoneyBoxes();
                return _moneyBoxes;
            }
            set
            {
                _moneyBoxes = value;
                OnPropertyChanged();
            }
        }

        private MoneyBox _selectedMoneyBox;

        public MoneyBox SelectedMoneyBox
        {
            get
            {
                return _selectedMoneyBox;
            }
            set
            {
                _selectedMoneyBox = value;
                OnPropertyChanged();
                GoToSelectedMoneyBox();
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

        private User _selectedUser;

        public User SelectedUser
        {
            get
            {
                return _selectedUser;
            }
            set
            {
                _selectedUser = value;
                OnPropertyChanged();
                GoToSelectedUser();
            }
        }

        private List<Category> _incomeCategories;

        public List<Category> IncomeCategories
        {
            get
            {
                if (_incomeCategories == null)
                    _incomeCategories = LoadIncomeCategories();
                return _incomeCategories;
            }
            set
            {
                _incomeCategories = value;
                OnPropertyChanged();
            }
        }

        private List<Category> _expenceCategories;

        public List<Category> ExpenceCategories
        {
            get
            {
                if (_expenceCategories == null)
                    _expenceCategories = LoadExpenceCategories();
                return _expenceCategories;
            }
            set
            {
                _expenceCategories = value;
                OnPropertyChanged();
            }
        }

        private Category _selectedCategory;

        public Category SelectedCategory
        {
            get
            {
                return _selectedCategory;
            }
            set
            {
                _selectedCategory = value;
                OnPropertyChanged();
                GoToSelectedCategory();
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

        private Currency _selectedCurrency;

        public Currency SelectedCurrency
        {
            get
            {
                return _selectedCurrency;
            }
            set
            {
                _selectedCurrency = value;
                OnPropertyChanged();
                GoToSelectedCurrency();
            }
        }

        #endregion

        #region Helping methods

        private List<MoneyBox> LoadMoneyBoxes()
        {
            return DataService.Get<MoneyBox>().GetAll();
        }

        private void GoToSelectedMoneyBox()
        {
            NavigationService.Navigate(typeof(MoneyBoxPage), new object[] { SelectedMoneyBox.Id });
        }

        private void GoToSelectedUser()
        {
            ;
        }

        private void GoToSelectedCategory()
        {
            ;
        }

        private void GoToSelectedCurrency()
        {
            ;
        }

        private List<User> LoadUsers()
        {
            return DataService.Get<User>().GetAll();
        }

        private List<Category> LoadIncomeCategories()
        {
            return DataService.Get<Category>().GetAll()
                .Where(c => c.Type == TransactionType.Income)
                .OrderBy(c => c.Title)
                .ToList();
        }

        private List<Category> LoadExpenceCategories()
        {
            return DataService.Get<Category>().GetAll()
                .Where(c => c.Type == TransactionType.Expence)
                .OrderBy(c => c.Title)
                .ToList();
        }

        private List<Currency> LoadCurrencies()
        {
            return DataService.Get<Currency>().GetAll();
        }

        #endregion
    }
}
