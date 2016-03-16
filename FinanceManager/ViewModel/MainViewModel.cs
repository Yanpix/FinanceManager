using FinanceManager.Common;
using FinanceManager.Infrastructure;
using FinanceManager.Model.DataAccess.Services;
using FinanceManager.Model.Entities;
using FinanceManager.View;
using System.Collections.Generic;
using System.Windows.Input;

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
            // Initialize properties
            _moneyBoxes = new List<MoneyBox>();
            _selectedMoneyBox = new MoneyBox();

            // Initialize commands
            CreateMoneyBoxCommand = new RelayCommand(CreateMoneyBox);
            EditUsersCommand = new RelayCommand(EditUsers);
            EditCurrenciesCommand = new RelayCommand(EditCurrencies);
            EditCategoriesCommand = new RelayCommand(EditCategories);
            DeleteAllMoneyBoxesCommand = new RelayCommand(DeleteAllMoneyBoxes);
        }

        #region Services

        // Data service for money boxes
        public IDataService<MoneyBox> MoneyBoxesDataService { get; set; }

        // Service for navigation
        public INavigationService NavigationService { get; set; }

        #endregion

        #region Commands

        public ICommand CreateMoneyBoxCommand { get; private set; }

        public ICommand EditUsersCommand { get; private set; }

        public ICommand EditCurrenciesCommand { get; private set; }

        public ICommand EditCategoriesCommand { get; private set; }

        public ICommand DeleteAllMoneyBoxesCommand { get; private set; }

        #endregion

        #region Commands implementation

        public void CreateMoneyBox()
        {
            NavigationService.Navigate(typeof(CreateMoneyBoxPage)); 
        }

        private void EditUsers()
        {
            ;
        }       

        private void EditCurrencies()
        {
            NavigationService.Navigate(typeof(CurrenciesPage));
        }        

        private void EditCategories()
        {
            NavigationService.Navigate(typeof(CategoriesPage));
        }     

        private void DeleteAllMoneyBoxes()
        {
            MoneyBoxesDataService.DeleteAll();
            LoadMoneyBoxes();
        }

        #endregion

        #region Properties

        private List<MoneyBox> _moneyBoxes;

        public List<MoneyBox> MoneyBoxes
        {
            get
            {
                LoadMoneyBoxes();
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

        #endregion

        #region Helping methods

        private void LoadMoneyBoxes()
        {
            MoneyBoxes = MoneyBoxesDataService.GetAll();
        }

        private void GoToSelectedMoneyBox()
        {
            NavigationService.Navigate(typeof(MoneyBoxPage), new object[] { SelectedMoneyBox });
        }

        #endregion
    }
}
