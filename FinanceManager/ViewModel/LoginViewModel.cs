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
    public class LoginViewModel : BindableBase
    {
        public LoginViewModel()
        {
            _navigationData = new Dictionary<string, object>();

            ValidationErrors = new ValidationErrors();

            LoginCommand = new DelegateCommand((e) => LoginAction(e));
        }

        private static Dictionary<string, object> _navigationData;

        #region Services

        public INavigationService NavigationService { get; set; }

        public IDataServicesProvider DataService { get; set; }

        #endregion

        #region Commands

        public ICommand LoginCommand { get; private set; }

        #endregion

        #region Commands implementation

        public void LoginAction(object e)
        {
            Validate();

            if (IsValid)
            {
                if (RepeatPasswordIsVisible)
                {
                    User user = new User();
                    user.Title = Title;
                    user.Password = Password;
                    DataService.Get<User>().Save(user);
                    _navigationData.Add("UserId", user.Id);
                }
                else
                {
                    User user = DataService.Get<User>().GetAll()
                        .Where(x => x.Title == Title && x.Password == Password).SingleOrDefault();
                    _navigationData.Add("UserId", user.Id);
                }
                
                NavigationService.Navigate(typeof(MainPage), _navigationData);
            }
            else
                return;
        }

        #endregion

        #region Properties

        public ValidationErrors ValidationErrors { get; set; }

        public bool IsValid { get; private set; }

        private string _title;

        public string Title
        {
            get
            {
                return _title;
            }
            set
            {
                _title = value;
                OnPropertyChanged();
                Password = "";
                RepeatPassword = "";
            }
        }

        private string _password = "";

        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                _password = value;
                OnPropertyChanged();
            }
        }

        private string _repeatPassword = "";

        public string RepeatPassword
        {
            get
            {
                return _repeatPassword;
            }
            set
            {
                _repeatPassword = value;
                OnPropertyChanged();
            }
        }

        private bool _repeatPasswordIsVisible = false;

        public bool RepeatPasswordIsVisible
        {
            get
            {
                return _repeatPasswordIsVisible;
            }
            set
            {
                _repeatPasswordIsVisible = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Helping methods

        private void Validate()
        {
            ValidationErrors.Clear();

            if (string.IsNullOrEmpty(Title))
            {
                RepeatPasswordIsVisible = false;
                ValidationErrors["Title"] = "title is required";
            }
            else
            {
                User existingUser = DataService.Get<User>().GetAll().Where(x => x.Title == Title).SingleOrDefault();

                if (existingUser != null)
                {
                    RepeatPasswordIsVisible = false;
                    if (!existingUser.Password.Equals(Password))
                    {
                        ValidationErrors["Password"] = "specified password isn't correct";
                    }
                }
                else
                {
                    RepeatPasswordIsVisible = true;
                    if (string.IsNullOrEmpty(Password) || string.IsNullOrWhiteSpace(Password))
                    {
                        ValidationErrors["Password"] = "password is required";
                    }
                    else if (string.IsNullOrEmpty(RepeatPassword) || string.IsNullOrWhiteSpace(RepeatPassword))
                    {
                        ValidationErrors["RepeatPassword"] = "repeat your password";
                    }
                    else if (!Password.Equals(RepeatPassword))
                    {
                        ValidationErrors["RepeatPassword"] = "specified passwords must be equal";
                    }
                }
            }

            IsValid = ValidationErrors.IsValid;

            OnPropertyChanged("IsValid");
            OnPropertyChanged("ValidationErrors");
        }

        #endregion
    }
}
