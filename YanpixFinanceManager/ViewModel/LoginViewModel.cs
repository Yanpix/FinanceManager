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
    public class LoginViewModel : BindableBase
    {
        #region Fields & Constructor

        private INavigationService _navigationService;

        private IEntityBaseService<Account> _accountService;

        private Dictionary<string, object> _navigationData;

        public LoginViewModel(INavigationService navigationService,
            IEntityBaseService<Account> accountService)
        {
            _navigationService = navigationService;
            _accountService = accountService;
            _navigationData = new Dictionary<string, object>();
        }

        #endregion

        #region Commands

        private ICommand _loginCommand;

        public ICommand LoginCommand
        {
            get
            {
                if (_loginCommand == null)
                    _loginCommand = new DelegateCommand((e) => LoginAction(e));
                return _loginCommand;
            }
            private set
            {
                _loginCommand = value;
            }
        }

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

        private void LoginAction(object e)
        {
            Validate();

            if (IsValid)
            {
                _navigationData.Add("AccountId", Account.Id);
                _navigationService.Navigate(typeof(MainPage), _navigationData);
            }
        }

        private void RegisterAction(object e)
        {
            _navigationService.Navigate(typeof(RegisterPage));
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

        public bool IsValid {
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

            Account existingAccount = _accountService.ExistingAccount(Account.Username, Account.Password);

            if (string.IsNullOrEmpty(Account.Username))
                ValidationErrors["Username"] = "Username is required";
            else if (Account.Username.Length > 50)
                ValidationErrors["Username"] = "Username must contain up to 50 characters";
            else if (existingAccount == null)
                ValidationErrors["Username"] = "Specify valid Credentials";

            if (string.IsNullOrEmpty(Account.Password))
                ValidationErrors["Password"] = "Password is required";
            else if (Account.Password.Length > 50)
                ValidationErrors["Password"] = "Password must contain up to 50 characters";
            else if (existingAccount == null)
                ValidationErrors["Password"] = "Specify valid Credentials";

            IsValid = ValidationErrors.IsValid;

            if (IsValid)
                Account = existingAccount;

            OnPropertyChanged("ValidationErrors");
        }

        #endregion
    }
}
