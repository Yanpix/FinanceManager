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
            ;
        }        

        private void EditCategories()
        {
            ;
        }       

        private void DeleteAllMoneyBoxes()
        {
            MoneyBoxesDataService.DeleteAll();
            LoadData();
        }

        #endregion

        #region Properties

        private List<MoneyBox> _moneyBoxes;

        public List<MoneyBox> MoneyBoxes
        {
            get
            {
                LoadData();
                return _moneyBoxes;
            }
            set
            {
                _moneyBoxes = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Helping methods

        private void LoadData()
        {
            MoneyBoxes = MoneyBoxesDataService.GetAll();
        }

        #endregion
    }
}
