using FinanceManager.Infrastructure;
using FinanceManager.Model.DataAccess.Providers;
using FinanceManager.Model.Entities;
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
    public class AddUserViewModel : BindableBase
    {
        public AddUserViewModel()
        {
            ValidationErrors = new ValidationErrors();

            SaveUserCommand = new DelegateCommand((e) => SaveUserAction(e));
            CancelUserCommand = new DelegateCommand((e) => CancelUserAction(e));
        }

        #region Services

        public INavigationService NavigationService { get; set; }

        public IDataServicesProvider DataService { get; set; }

        #endregion

        #region Commands

        public ICommand SaveUserCommand { get; private set; }

        public ICommand CancelUserCommand { get; private set; }

        #endregion

        #region Commands implementation

        public void SaveUserAction(object e)
        {
            Validate();

            if (IsValid)
            {
                MoneyBoxToUser mapping = new MoneyBoxToUser
                {
                    MoneyBoxId = MoneyBox.Id,
                    UserId = SelectedUser.Id,
                    CanModify = CanModifyMoneyBox,
                    CanRealize = CanRealizeTransactions,
                    IsOwner = false
                };

                DataService.Get<MoneyBoxToUser>().Save(mapping);

                NavigationService.Navigate(typeof(CreateMoneyBoxPage));
            }
            else
                return;
        }

        public void CancelUserAction(object e)
        {
            NavigationService.Navigate(typeof(CreateMoneyBoxPage));
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
                    _moneyBox = LoadMoneyBox();
                return _moneyBox;
            }
            set
            {
                _moneyBox = value;
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

        private bool _canModifyMoneyBox;

        public bool CanModifyMoneyBox
        {
            get
            {
                return _canModifyMoneyBox;
            }
            set
            {
                _canModifyMoneyBox = value;
                OnPropertyChanged();
            }
        }

        private bool _canRealizeTransactions;

        public bool CanRealizeTransactions
        {
            get
            {
                return _canRealizeTransactions;
            }
            set
            {
                _canRealizeTransactions = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Helping methods

        private MoneyBox LoadMoneyBox()
        {
            object currentMoneyBoxId = NavigationService.GetNavigationData("MoneyBoxId");

            MoneyBox currentMoneyBox = DataService.Get<MoneyBox>().Get((int)currentMoneyBoxId);

            return currentMoneyBox;
        }

        private User LoadCurrentUser()
        {
            object currentUserId = NavigationService.GetNavigationData("UserId");

            User currentUser = DataService.Get<User>().Get((int)currentUserId);

            return currentUser;
        }

        private List<User> LoadUsers()
        {
            return DataService.Get<User>().GetAll().Where(x => x.Id != CurrentUser.Id).ToList();
        }

        private void Validate()
        {
            ValidationErrors.Clear();

            if (SelectedUser == null)
            {
                ValidationErrors["SelectedUser"] = "user is required";
            }

            IsValid = ValidationErrors.IsValid;

            OnPropertyChanged("IsValid");
            OnPropertyChanged("ValidationErrors");
        }

        #endregion
    }
}
