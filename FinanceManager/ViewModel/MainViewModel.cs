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
    public class MainViewModel : BindableBase
    {
        // View model constructor
        public MainViewModel()
        {
            _navigationData = new Dictionary<string, object>();

            _selectedPivotItemInit = false;

            CreateCommand = new DelegateCommand((e) => CreateAction(e));
            DeleteCommand = new DelegateCommand((item) => DeleteAction(item));
            DeleteAllCommand = new DelegateCommand((e) => DeleteAllAction(e));
        }

        private static Dictionary<string, object> _navigationData;

        #region Services

        public INavigationService NavigationService { get; set; }

        public IDataServicesProvider DataService { get; set; }

        #endregion

        #region Commands

        public ICommand CreateCommand { get; private set; }

        public ICommand DeleteCommand { get; private set; }

        public ICommand DeleteAllCommand { get; private set; }

        #endregion

        #region Commands implementation

        public void CreateAction(object e)
        {
            _navigationData.Add("SelectedPivotItemOnMainPage", SelectedPivotItem);

            switch (_selectedPivotItem)
            {
                case 0:                  
                    NavigationService.Navigate(typeof(CreateMoneyBoxPage), _navigationData);
                    break;
                case 1:
                    NavigationService.Navigate(typeof(CreateCategoryPage), _navigationData);
                    break;
                case 2:
                    NavigationService.Navigate(typeof(CreateCurrencyPage), _navigationData);
                    break;
            }
        }

        public void DeleteAction(object e)
        {
            switch (_selectedPivotItem)
            {
                case 0:
                    MoneyBox moneyBox = e as MoneyBox;
                    DataService.Get<MoneyBox>().Delete(moneyBox.Id);
                    MoneyBoxes = LoadMoneyBoxes();
                    break;
                case 1:
                    Category category = e as Category;
                    DataService.Get<Category>().Delete(category.Id);
                    IncomeCategories = LoadIncomeCategories();
                    ExpenceCategories = LoadExpenceCategories();
                    break;
                case 2:
                    Currency currency = e as Currency;
                    DataService.Get<Currency>().Delete(currency.Id);
                    Currencies = LoadCurrencies();
                    break;
            }
        }

        public void DeleteAllAction(object e)
        {
            switch (_selectedPivotItem)
            {
                case 0:
                    DataService.Get<MoneyBox>().DeleteAll();
                    MoneyBoxes = LoadMoneyBoxes();
                    break;
                case 1:
                    DataService.Get<Category>().DeleteAll();
                    IncomeCategories = LoadIncomeCategories();
                    ExpenceCategories = LoadExpenceCategories();
                    break;
                case 2:
                    DataService.Get<Currency>().DeleteAll();
                    Currencies = LoadCurrencies();
                    break;
            }
        }

        #endregion

        #region Properties

        private User _currentUser;

        public User CurrentUser
        {
            get
            {
                if (_currentUser == null)
                    _currentUser = LoadCurrentUser();
                return _currentUser;
            }
            set
            {
                _currentUser = value;
                OnPropertyChanged();
            }
        }

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
                }
                return _selectedPivotItem;
            }
            set
            {
                _selectedPivotItem = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Helping methods

        private List<MoneyBox> LoadMoneyBoxes()
        {
            return DataService.Get<MoneyBox>().GetAll().Where(x => x.Users.Any(s => s.Id == CurrentUser.Id)).ToList();
        }

        private void GoToSelectedMoneyBox()
        {
            _navigationData.Add("MoneyBoxId", SelectedMoneyBox.Id);
            _navigationData.Add("SelectedPivotItemOnMainPage", SelectedPivotItem);

            NavigationService.Navigate(typeof(MoneyBoxPage), _navigationData);
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

        private int LoadSelectedPivotItem(int currentPivotItem)
        {
            object selectedPivotItem = NavigationService.GetNavigationData("SelectedPivotItemOnMainPage");

            if (selectedPivotItem != null && currentPivotItem != (int)selectedPivotItem)
                return (int)selectedPivotItem;
            else
                return currentPivotItem;
        }

        private User LoadCurrentUser()
        {
            object currentUserId = NavigationService.GetNavigationData("UserId");

            User currentUser = DataService.Get<User>().Get((int)currentUserId);

            return currentUser;
        }

        #endregion
    }
}
