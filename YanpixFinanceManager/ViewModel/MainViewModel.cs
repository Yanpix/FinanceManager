using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using YanpixFinanceManager.View;
using YanpixFinanceManager.ViewModel.Common;

namespace YanpixFinanceManager.ViewModel
{
    public class MainViewModel : BindableBase
    {
        #region Fields & Constructor

        private INavigationService _navigationService;

        private IPlatformEvents _platformEvents;

        public MainViewModel(INavigationService navigationService, IPlatformEvents platformEvents)
        {
            _navigationService = navigationService;
            _platformEvents = platformEvents;

            _platformEvents.BackButtonPressed += BackButtonPressed;
        }

        #endregion

        #region Commands

        private ICommand _moneyBoxesCommand;

        public ICommand MoneyBoxesCommand
        {
            get
            {
                if (_moneyBoxesCommand == null)
                    _moneyBoxesCommand = new DelegateCommand((e) => MoneyBoxesAction(e));
                return _moneyBoxesCommand;
            }
            private set
            {
                _moneyBoxesCommand = value;
            }
        }

        private ICommand _categoriesCommand;

        public ICommand CategoriesCommand
        {
            get
            {
                if (_categoriesCommand == null)
                    _categoriesCommand = new DelegateCommand((e) => CategoriesAction(e));
                return _categoriesCommand;
            }
            private set
            {
                _categoriesCommand = value;
            }
        }

        private ICommand _currenciesCommand;

        public ICommand CurrenciesCommand
        {
            get
            {
                if (_currenciesCommand == null)
                    _currenciesCommand = new DelegateCommand((e) => CurrenciesAction(e));
                return _currenciesCommand;
            }
            private set
            {
                _currenciesCommand = value;
            }
        }

        private ICommand _activitiesCommand;

        public ICommand ActivitiesCommand
        {
            get
            {
                if (_activitiesCommand == null)
                    _activitiesCommand = new DelegateCommand((e) => ActivitiesAction(e));
                return _activitiesCommand;
            }
            private set
            {
                _activitiesCommand = value;
            }
        }

        private ICommand _settingsCommand;

        public ICommand SettingsCommand
        {
            get
            {
                if (_settingsCommand == null)
                    _settingsCommand = new DelegateCommand((e) => SettingsAction(e));
                return _settingsCommand;
            }
            private set
            {
                _settingsCommand = value;
            }
        }

        #endregion

        #region Command Actions

        private void MoneyBoxesAction(object e)
        {
            _navigationService.Navigate(typeof(MoneyBoxesPage));
        }

        private void CategoriesAction(object e)
        {
            _navigationService.Navigate(typeof(CategoriesPage));
        }

        private void CurrenciesAction(object e)
        {
            _navigationService.Navigate(typeof(CurrenciesPage));
        }

        private void ActivitiesAction(object e)
        {
            _navigationService.Navigate(typeof(MainPage));
        }

        private void SettingsAction(object e)
        {
            _navigationService.Navigate(typeof(MainPage));
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
