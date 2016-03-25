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
            _navigationData = new Dictionary<string, object>();
            _selectedMoneyBoxes = new List<MoneyBox>();
            _selectedPivotItemInit = false;

            // Initialize commands
            CreateMoneyBoxCommand = new RelayCommand(CreateMoneyBox);
            DeleteMoneyBoxCommand = new DelegateCommand((e) => DeleteMoneyBox(e));
            SelectMoneyBoxCommand = new DelegateCommand((e) => SelectMoneyBox(e));
            DeleteAllMoneyBoxesCommand = new RelayCommand(DeleteAllMoneyBoxes);
            DeleteSelectedMoneyBoxesCommand = new RelayCommand(DeleteSelectedMoneyBoxes);

            CreateUserCommand = new RelayCommand(CreateUser);
            DeleteAllUsersCommand = new RelayCommand(DeleteAllUsers);

            CreateCategoryCommand = new RelayCommand(CreateCategory);
            DeleteAllCategoriesCommand = new RelayCommand(DeleteAllCategories);

            CreateCurrencyCommand = new RelayCommand(CreateCurrency);
            DeleteAllCurrenciesCommand = new RelayCommand(DeleteAllCurrencies);           
        }

        private static Dictionary<string, object> _navigationData;

        private static List<MoneyBox> _selectedMoneyBoxes;

        #region Services

        public INavigationService NavigationService { get; set; }

        public IDataServicesProvider DataService { get; set; }

        #endregion

        #region Commands

        public ICommand CreateMoneyBoxCommand { get; private set; }

        public ICommand DeleteMoneyBoxCommand { get; private set; }

        public ICommand SelectMoneyBoxCommand { get; private set; }

        public ICommand DeleteAllMoneyBoxesCommand { get; private set; }

        public ICommand DeleteSelectedMoneyBoxesCommand { get; private set; }

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
            _navigationData.Add("SelectedPivotItemOnMainPage", SelectedPivotItem);
            NavigationService.Navigate(typeof(CreateMoneyBoxPage), _navigationData); 
        }   

        public void DeleteMoneyBox(object e)
        {
            MoneyBox moneyBox = e as MoneyBox;
            DataService.Get<MoneyBox>().Delete(moneyBox.Id);
            MoneyBoxes = LoadMoneyBoxes();
        }

        private void SelectMoneyBox(object e)
        {
            MoneyBox moneyBox = e as MoneyBox;
            if (_selectedMoneyBoxes.Contains(moneyBox))
            {
                _selectedMoneyBoxes.Remove(moneyBox);
            }
            else
                _selectedMoneyBoxes.Add(moneyBox);
        }

        private void DeleteAllMoneyBoxes()
        {
            DataService.Get<MoneyBox>().DeleteAll();
            MoneyBoxes = LoadMoneyBoxes();
        }

        private void DeleteSelectedMoneyBoxes()
        {
            if (_selectedMoneyBoxes.Count > 0)
            {
                foreach (MoneyBox moneyBox in _selectedMoneyBoxes)
                {
                    DataService.Get<MoneyBox>().Delete(moneyBox.Id);
                }

                MoneyBoxes = LoadMoneyBoxes();
            }
        }

        public void CreateUser()
        {
            _navigationData.Add("SelectedPivotItemOnMainPage", SelectedPivotItem);
            NavigationService.Navigate(typeof(CreateUserPage), _navigationData);
        }

        private void DeleteAllUsers()
        {
            DataService.Get<User>().DeleteAll();
            Users = LoadUsers();
        }

        public void CreateCategory()
        {
            _navigationData.Add("SelectedPivotItemOnMainPage", SelectedPivotItem);
            NavigationService.Navigate(typeof(CreateCategoryPage), _navigationData);
        }

        private void DeleteAllCategories()
        {
            DataService.Get<Category>().DeleteAll();
            IncomeCategories = LoadIncomeCategories();
            ExpenceCategories = LoadExpenceCategories();
        }

        public void CreateCurrency()
        {
            _navigationData.Add("SelectedPivotItemOnMainPage", SelectedPivotItem);
            NavigationService.Navigate(typeof(CreateCurrencyPage), _navigationData);
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

        private bool _selectedPivotItemInit;

        private int _selectedPivotItem;

        public int SelectedPivotItem
        {
            get
            {
                if (!_selectedPivotItemInit)
                {
                    _selectedPivotItem = LoadSelectedPivotItem(_selectedPivotItem);
                    _selectedPivotItemInit = true;
                    SwitchCommandBarsVisibility();
                }
                return _selectedPivotItem;
            }
            set
            {
                _selectedPivotItem = value;
                OnPropertyChanged();
                SwitchCommandBarsVisibility();
            }
        }

        private bool _moneyBoxesCommandBarVisible;

        public bool MoneyBoxesCommandBarVisible
        {
            get
            {
                return _moneyBoxesCommandBarVisible;
            }
            set
            {
                _moneyBoxesCommandBarVisible = value;
                OnPropertyChanged();
            }
        }

        private bool _usersCommandBarVisible;

        public bool UsersCommandBarVisible
        {
            get
            {
                return _usersCommandBarVisible;
            }
            set
            {
                _usersCommandBarVisible = value;
                OnPropertyChanged();
            }
        }

        private bool _currenciesCommandBarVisible;

        public bool CurrenciesCommandBarVisible
        {
            get
            {
                return _currenciesCommandBarVisible;
            }
            set
            {
                _currenciesCommandBarVisible = value;
                OnPropertyChanged();
            }
        }

        private bool _categoriesCommandBarVisible;

        public bool CategoriesCommandBarVisible
        {
            get
            {
                return _categoriesCommandBarVisible;
            }
            set
            {
                _categoriesCommandBarVisible = value;
                OnPropertyChanged();
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
            _navigationData.Add("MoneyBoxId", SelectedMoneyBox.Id);
            _navigationData.Add("SelectedPivotItemOnMainPage", SelectedPivotItem);

            NavigationService.Navigate(typeof(MoneyBoxPage), _navigationData);
        }

        private void GoToSelectedUser()
        {
            ;
        }

        private void GoToSelectedCategory()
        {
            _navigationData.Add("CategoryId", SelectedCategory.Id);
            _navigationData.Add("SelectedPivotItemOnMainPage", SelectedPivotItem);

            NavigationService.Navigate(typeof(CategoryPage), _navigationData);
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

        private void SwitchCommandBarsVisibility()
        {
            switch (_selectedPivotItem)
            {
                case 0:
                    MoneyBoxesCommandBarVisible = true;
                    UsersCommandBarVisible = false;
                    CategoriesCommandBarVisible = false;
                    CurrenciesCommandBarVisible = false;
                    break;
                case 1:
                    MoneyBoxesCommandBarVisible = false;
                    UsersCommandBarVisible = true;
                    CategoriesCommandBarVisible = false;
                    CurrenciesCommandBarVisible = false;
                    break;
                case 2:
                    MoneyBoxesCommandBarVisible = false;
                    UsersCommandBarVisible = false;
                    CategoriesCommandBarVisible = true;
                    CurrenciesCommandBarVisible = false;
                    break;
                case 3:
                    MoneyBoxesCommandBarVisible = false;
                    UsersCommandBarVisible = false;
                    CategoriesCommandBarVisible = false;
                    CurrenciesCommandBarVisible = true;
                    break;
                default:
                    break;
            }
        }

        private int LoadSelectedPivotItem(int currentPivotItem)
        {
            object selectedPivotItem = NavigationService.GetNavigationData("SelectedPivotItemOnMainPage");

            if (selectedPivotItem != null && currentPivotItem != (int)selectedPivotItem)
                return (int)selectedPivotItem;
            else
                return currentPivotItem;
        }

        #endregion
    }
}
