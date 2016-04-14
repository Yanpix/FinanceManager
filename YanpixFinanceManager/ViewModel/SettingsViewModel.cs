using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using YanpixFinanceManager.Model.DataAccess.Services;
using YanpixFinanceManager.Model.Entities;
using YanpixFinanceManager.View;
using YanpixFinanceManager.ViewModel.Common;

namespace YanpixFinanceManager.ViewModel
{
    public class SettingsViewModel : BindableBase
    {
        #region Fields & Constructor

        private INavigationService _navigationService;

        private IEntityBaseService<Setting> _settingsService;

        private IPlatformEvents _platformEvents;

        private Dictionary<string, object> _navigationData;

        public SettingsViewModel(INavigationService navigationService,
            IEntityBaseService<Setting> settingsService,
            IPlatformEvents platformEvents)
        {
            _navigationService = navigationService;
            _settingsService = settingsService;
            _navigationData = new Dictionary<string, object>();
            _platformEvents = platformEvents;

            _platformEvents.BackButtonPressed += BackButtonPressed;
        }

        #endregion

        #region Commands

        private ICommand _saveCommand;

        public ICommand SaveCommand
        {
            get
            {
                if (_saveCommand == null)
                    _saveCommand = new DelegateCommand((e) => SaveAction(e));
                return _saveCommand;
            }
            private set
            {
                _saveCommand = value;
            }
        }

        #endregion

        #region Command Actions

        private void SaveAction(object e)
        {
            _settingsService.Insert(Setting, true);

            _navigationService.Navigate(typeof(MainPage));
        }

        #endregion

        #region Properies

        private Setting _setting;

        public Setting Setting
        {
            get
            {
                if (_setting == null)
                    _setting = _settingsService.Get(1);

                return _setting;
            }
            set
            {
                _setting = value;
                OnPropertyChanged();
            }
        }

        private List<int> _days;

        public List<int> Days
        {
            get
            {
                if (_days == null)
                    _days = new List<int>(Enumerable.Range(1, 31));

                return _days;
            }
            set
            {
                _days = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Platform Events

        private void BackButtonPressed(object sender, EventArgs e)
        {
            _navigationService.Navigate(typeof(MainPage));

            _platformEvents.BackButtonPressed -= BackButtonPressed;
        }

        #endregion
    }
}
