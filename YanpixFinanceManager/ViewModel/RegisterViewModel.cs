using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using YanpixFinanceManager.Model.DataAccess.Extensions;
using YanpixFinanceManager.Model.DataAccess.Services;
using YanpixFinanceManager.Model.Entities;
using YanpixFinanceManager.View;
using YanpixFinanceManager.ViewModel.Common;

namespace YanpixFinanceManager.ViewModel
{
    public class RegisterViewModel : BindableBase
    {
        #region Fields & Constructor

        private INavigationService _navigationService;

        private IEntityBaseService<Account> _accountService;

        private IPlatformEvents _platformEvents;

        private Dictionary<string, object> _navigationData;

        public RegisterViewModel(INavigationService navigationService,
            IEntityBaseService<Account> accountService,
            IPlatformEvents platformEvents)
        {
            _navigationService = navigationService;
            _accountService = accountService;
            _platformEvents = platformEvents;
            _navigationData = new Dictionary<string, object>();

            _platformEvents.BackButtonPressed += BackButtonPressed;
        }

        #endregion

        #region Commands

        private ICommand _registerCommand;

        public ICommand RegisterCommand
        {
            get
            {
                if (_registerCommand == null)
                    _registerCommand = new DelegateCommand((e) => RegisterAction(e));
                return _registerCommand;
            }
            private set
            {
                _registerCommand = value;
            }
        }

        #endregion

        #region Command Actions

        private void RegisterAction(object e)
        {
            Validate();

            if (IsValid)
            {
                _accountService.Insert(Account, false);

                _navigationData.Add("AccountId", Account.Id);
                _navigationService.Navigate(typeof(MainPage), _navigationData);
            }
        }

        #endregion

        #region Properties

        private Account _account;

        public Account Account
        {
            get
            {
                if (_account == null)
                    _account = new Account();
                return _account;
            }
            set
            {
                _account = value;
                OnPropertyChanged();
            }
        }

        private string _repeatPassword = "";

        public string RepeatPassword
        {
            get { return _repeatPassword; }
            set
            {
                _repeatPassword = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Validation

        private ValidationErrors _validationErrors;

        public ValidationErrors ValidationErrors
        {
            get
            {
                if (_validationErrors == null)
                    _validationErrors = new ValidationErrors();
                return _validationErrors;
            }
            set
            {
                _validationErrors = value;
                OnPropertyChanged();
            }
        }

        private bool _isValid = false;

        public bool IsValid
        {
            get
            {
                return _isValid;
            }
            private set
            {
                _isValid = value;
                OnPropertyChanged();
            }
        }

        private void Validate()
        {
            ValidationErrors.Clear();

            Account existingAccount = _accountService.ExistingAccount(Account.Username);

            if (string.IsNullOrEmpty(Account.Username))
                ValidationErrors["Username"] = "Username is required";
            else if (Account.Username.Length > 50)
                ValidationErrors["Username"] = "Username must contain up to 50 characters";
            else if (existingAccount != null)
                ValidationErrors["Username"] = "Account already exists";

            if (string.IsNullOrEmpty(Account.Password))
                ValidationErrors["Password"] = "Password is required";
            else if (Account.Password.Length > 50)
                ValidationErrors["Password"] = "Password must contain up to 50 characters";

            if (!Account.Password.Equals(RepeatPassword))
                ValidationErrors["RepeatPassword"] = "Repeat specified password";

            if (string.IsNullOrEmpty(Account.Email))
                ValidationErrors["Email"] = "Email Address is required";
            else if (Account.Email.Length > 50)
                ValidationErrors["Email"] = "Email Address must contain up to 50 characters";

            IsValid = ValidationErrors.IsValid;

            OnPropertyChanged("ValidationErrors");
        }

        #endregion

        #region Platform Events

        private void BackButtonPressed(object sender, EventArgs e)
        {
            _navigationService.Navigate(typeof(LoginPage));

            _platformEvents.BackButtonPressed -= BackButtonPressed;
        }

        #endregion
    }
}
