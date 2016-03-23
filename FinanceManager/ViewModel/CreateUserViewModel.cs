using FinanceManager.Common;
using FinanceManager.Infrastructure;
using FinanceManager.Model.DataAccess.Providers;
using FinanceManager.Model.Entities;
using FinanceManager.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FinanceManager.ViewModel
{
    public class CreateUserViewModel : BaseViewModel
    {
        public CreateUserViewModel()
        {
            SaveUserCommand = new RelayCommand(SaveUser);
            CancelUserCommand = new RelayCommand(CancelUser);
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
            DataService.Get<User>().Save(User);

            NavigationService.Navigate(typeof(MainPage));
        }

        public void CancelUser()
        {
            NavigationService.Navigate(typeof(MainPage));
        }

        #endregion

        #region Properties

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

        #endregion
    }
}
