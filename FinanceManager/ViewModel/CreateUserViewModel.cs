using FinanceManager.Common;
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
    public class CreateUserViewModel : BindableBase
    {
        public CreateUserViewModel()
        {
            SaveUserCommand = new RelayCommand(SaveUser);
            CancelUserCommand = new RelayCommand(CancelUser);

            ValidationErrors = new ValidationErrors();
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

        public void SaveUser()
        {
            Validate();

            if (IsValid)
            {
                DataService.Get<User>().Save(User);

                NavigationService.Navigate(typeof(MainPage));
            }
            else
                return;
        }

        public void CancelUser()
        {
            NavigationService.Navigate(typeof(MainPage));
        }

        #endregion

        #region Properties

        public ValidationErrors ValidationErrors { get; set; }

        public bool IsValid { get; private set; }

        private User _user;

        public User User
        {
            get
            {
                if (_user == null)
                    _user = new User();
                return _user;
            }
            set
            {
                _user = value;
                OnPropertyChanged();
            }
        }

        private void Validate()
        {
            ValidationErrors.Clear();

            User user = DataService.Get<User>().GetAll().Where(x => x.Title.Equals(User.Title)).SingleOrDefault();

            if (string.IsNullOrEmpty(User.Title))
            {
                ValidationErrors["Title"] = "title is required";
            }
            else if (User.Title.Length > 20)
            {
                ValidationErrors["Title"] = "title's length should not exceed 20 characters";
            }
            else if (user != null)
            {
                ValidationErrors["Title"] = "specified title already exists";
            }

            IsValid = ValidationErrors.IsValid;

            OnPropertyChanged("IsValid");
            OnPropertyChanged("ValidationErrors");
        }

        #endregion
    }
}
