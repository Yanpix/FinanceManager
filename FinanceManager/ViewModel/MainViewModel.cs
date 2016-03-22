using FinanceManager.Common;
using FinanceManager.Infrastructure;
using FinanceManager.Model.DataAccess.Providers;
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
            // Initialize commands
            CreateMoneyBoxCommand = new RelayCommand(CreateMoneyBox);
            EditUsersCommand = new RelayCommand(EditUsers);
            EditCurrenciesCommand = new RelayCommand(EditCurrencies);
            EditCategoriesCommand = new RelayCommand(EditCategories);
            DeleteAllMoneyBoxesCommand = new RelayCommand(DeleteAllMoneyBoxes);
        }

        #region Services

        public INavigationService NavigationService { get; set; }

        public IDataServicesProvider DataService { get; set; }

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
            DataService.Get<MoneyBox>().DeleteAll();
            MoneyBoxes = LoadMoneyBoxes();
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

        #endregion
    }
}
