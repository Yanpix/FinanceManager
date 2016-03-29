using FinanceManager.Common;
using FinanceManager.Infrastructure;
using FinanceManager.Model.DataAccess.Providers;
using FinanceManager.Model.DataAccess.Services;
using FinanceManager.Model.Entities;
using FinanceManager.View;
using FinanceManager.ViewModel.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace FinanceManager.ViewModel
{
    /// <summary>
    /// View model for CreateMoneyBoxPage, implements BaseViewModel
    /// </summary>
    public class CreateMoneyBoxViewModel : BindableBase
    {
        // View model constructor
        public CreateMoneyBoxViewModel()
        {
            ValidationErrors = new ValidationErrors();

            // Initialize commands
            SaveMoneyBoxCommand = new DelegateCommand((e) => SaveMoneyBoxAction(e));
            CancelMoneyBoxCommand = new DelegateCommand((e) => CancelMoneyBoxAction(e));
            AddUserCommand = new DelegateCommand((e) => AddUserAction(e));
            DeleteAllUsersCommand = new DelegateCommand((e) => DeleteAllUsersAction(e));
            DeleteUserCommand = new DelegateCommand((item) => DeleteUserAction(item));
        }

        #region Services

        public INavigationService NavigationService { get; set; }

        public IDataServicesProvider DataService { get; set; }

        #endregion

        #region Commands

        public ICommand SaveMoneyBoxCommand { get; private set; }

        public ICommand CancelMoneyBoxCommand { get; private set; }

        public ICommand AddUserCommand { get; private set; }

        public ICommand DeleteAllUsersCommand { get; private set; }

        public ICommand DeleteUserCommand { get; private set; }

        #endregion

        #region Commands implementation

        public void SaveMoneyBoxAction(object e)
        {
            Validate();

            if (ValidationErrors.IsValid)
            {
                MoneyBox.CreationDate = DateTime.Now;
                MoneyBox.LastModifiedDate = DateTime.Now;
                MoneyBox.Balance = 0.0M;

                DataService.Get<MoneyBox>().Save(MoneyBox);

                _navigationData.Add("MoneyBoxId", MoneyBox.Id);

                NavigationService.Navigate(typeof(MoneyBoxPage), _navigationData);
            }
            else
                return;
        }       

        public void CancelMoneyBoxAction(object e)
        {
            NavigationService.Navigate(typeof(MainPage));
        }

        public void AddUserAction(object e)
        {
            _navigationData.Add("MoneyBoxId", MoneyBox.Id);
            _navigationData.Add("SelectedPivotItemOnCreateMoneyBoxPage", SelectedPivotItem);
            NavigationService.Navigate(typeof(AddUserPage), _navigationData);
        }

        public void DeleteAllUsersAction(object e)
        {

        }

        public void DeleteUserAction(object e)
        {

        }

        #endregion

        #region Properties

        private Dictionary<string, object> _navigationData = new Dictionary<string, object>();

        public ValidationErrors ValidationErrors { get; set; }

        public bool IsValid { get; private set; }

        private MoneyBox _moneyBox;

        public MoneyBox MoneyBox
        {
            get
            {
                if (_moneyBox == null)
                    _moneyBox = CreateMoneyBox();
                return _moneyBox;
            }
            set
            {
                _moneyBox = value;
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
            }
        }

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

        private bool _selectedPivotItemInit = false;

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

        private bool _moneyBoxCommandsVisible;

        public bool MoneyBoxCommandsVisible
        {
            get
            {
                return _moneyBoxCommandsVisible;
            }
            set
            {
                _moneyBoxCommandsVisible = value;
                OnPropertyChanged();
            }
        }

        private bool _usersCommandsVisible;

        public bool UsersCommandsVisible
        {
            get
            {
                return _usersCommandsVisible;
            }
            set
            {
                _usersCommandsVisible = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Helping methods

        private MoneyBox CreateMoneyBox()
        {
            object moneyBoxId = NavigationService.GetNavigationData("MoneyBoxId");

            if (moneyBoxId != null)
            {
                return DataService.Get<MoneyBox>().Get((int)moneyBoxId);
            }
            else
            {
                MoneyBox newMoneyBox = new MoneyBox();

                DataService.Get<MoneyBox>().Save(newMoneyBox);

                return newMoneyBox;
            }
        }

        private List<Currency> LoadCurrencies()
        {
            return DataService.Get<Currency>().GetAll();
        }

        private List<User> LoadUsers()
        {
            return DataService.Get<User>().GetAll().Where(x => x.MoneyBoxes.Any(s => s.Id == MoneyBox.Id)).ToList();
        }

        private User LoadCurrentUser()
        {
            object currentUserId = NavigationService.GetNavigationData("UserId");

            User currentUser = DataService.Get<User>().Get((int)currentUserId);

            return currentUser;
        }

        private int LoadSelectedPivotItem(int currentPivotItem)
        {
            object selectedPivotItem = NavigationService.GetNavigationData("SelectedPivotItemOnCreateMoneyBoxPage");

            if (selectedPivotItem != null && currentPivotItem != (int)selectedPivotItem)
                return (int)selectedPivotItem;
            else
                return currentPivotItem;
        }

        private void SwitchCommandBarsVisibility()
        {
            switch (_selectedPivotItem)
            {
                case 0:
                    MoneyBoxCommandsVisible = true;
                    UsersCommandsVisible = false;
                    break;
                case 1:
                    MoneyBoxCommandsVisible = false;
                    UsersCommandsVisible = true;
                    break;
            }
        }

        private void Validate()
        {
            ValidationErrors.Clear();

            MoneyBox moneyBox = DataService.Get<MoneyBox>().GetAll().Where(x => x.Title.Equals(MoneyBox.Title)).SingleOrDefault();

            if (string.IsNullOrEmpty(MoneyBox.Title))
            {
                ValidationErrors["Title"] = "title is required";
            }
            else if (MoneyBox.Title.Length > 50)
            {
                ValidationErrors["Title"] = "title's length should not exceed 50 characters";
            }
            else if(moneyBox != null)
            {
                ValidationErrors["Title"] = "specified title already exists";
            }

            if (MoneyBox.PrimaryCurrency == null)
            {
                ValidationErrors["Currency"] = "primary currency is required";
            }

            IsValid = ValidationErrors.IsValid;

            OnPropertyChanged("IsValid");
            OnPropertyChanged("ValidationErrors");
        }

        #endregion
    }
}
