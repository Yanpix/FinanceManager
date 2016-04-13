using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using YanpixFinanceManager.Model.DataAccess.Extensions;
using YanpixFinanceManager.Model.DataAccess.Services;
using YanpixFinanceManager.Model.Entities;
using YanpixFinanceManager.Model.Entities.Enums;
using YanpixFinanceManager.View;
using YanpixFinanceManager.ViewModel.Common;

namespace YanpixFinanceManager.ViewModel
{
    public class MoneyBoxesViewModel : BindableBase
    {
        #region Fields & Constructor

        private INavigationService _navigationService;

        private IEntityBaseService<ReportingPeriod> _reportingPeriodService;

        private IEntityBaseService<MoneyBox> _moneyBoxesService;

        private IPlatformEvents _platformEvents;

        private Dictionary<string, object> _navigationData;

        public MoneyBoxesViewModel(INavigationService navigationService,
            IEntityBaseService<MoneyBox> moneyBoxesService,
            IEntityBaseService<ReportingPeriod> reportingPeriodService,
            IPlatformEvents platformEvents)
        {
            _navigationService = navigationService;
            _moneyBoxesService = moneyBoxesService;
            _reportingPeriodService = reportingPeriodService;
            _platformEvents = platformEvents;
            _navigationData = new Dictionary<string, object>();

            _platformEvents.BackButtonPressed += BackButtonPressed;
        }

        #endregion

        #region Commands

        private ICommand _createNewCommand;

        public ICommand CreateNewCommand
        {
            get
            {
                if (_createNewCommand == null)
                    _createNewCommand = new DelegateCommand((e) => CreateNewAction(e));
                return _createNewCommand;
            }
            private set
            {
                _createNewCommand = value;
            }
        }

        #endregion

        #region Command Actions

        private void CreateNewAction(object e)
        {
            _navigationService.Navigate(typeof(CreateMoneyBoxPage));
        }

        #endregion

        #region Properties

        private ObservableCollection<MoneyBoxWithBars> _moneyBoxes;

        public ObservableCollection<MoneyBoxWithBars> MoneyBoxes
        {
            get
            {
                if (_moneyBoxes == null)
                    UpdateMoneyBoxes();

                return _moneyBoxes;
            }
            set
            {
                _moneyBoxes = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Helping Methods

        private void UpdateMoneyBoxes()
        {
            MoneyBoxes = new ObservableCollection<MoneyBoxWithBars>(
                _moneyBoxesService.GetAll()
                .Select(x => new MoneyBoxWithBars()
                {
                    Income = _reportingPeriodService.GetIncomeForLastMonth(x.Id),
                    Expence = _reportingPeriodService.GetExpenceForLastMonth(x.Id),
                    Balance = Math.Abs(_reportingPeriodService.GetBalanceForLastMonth(x.Id)),
                    Budget = _reportingPeriodService.GetBudgetForLastMonth(x.Id),
                    Available = Math.Abs(_reportingPeriodService.GetBudgetBalanceForLastMonth(x.Id)),
                    Image = x.PrimaryCurrency.Image,
                    Title = x.Title
                }));
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
