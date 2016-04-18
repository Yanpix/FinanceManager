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
    public class MoneyBoxViewModel : BindableBase
    {
        #region Fields & Constructor

        private INavigationService _navigationService;

        private IEntityBaseService<ReportingPeriod> _reportingPeriodService;

        private IEntityBaseService<MoneyBox> _moneyBoxService;

        private IPlatformEvents _platformEvents;

        private Dictionary<string, object> _navigationData;

        public MoneyBoxViewModel(INavigationService navigationService,
            IEntityBaseService<MoneyBox> moneyBoxService,
            IEntityBaseService<ReportingPeriod> reportingPeriodService,
            IPlatformEvents platformEvents)
        {
            _navigationService = navigationService;
            _moneyBoxService = moneyBoxService;
            _reportingPeriodService = reportingPeriodService;
            _platformEvents = platformEvents;
            _navigationData = new Dictionary<string, object>();

            _platformEvents.BackButtonPressed += BackButtonPressed;
        }

        #endregion

        #region Commands

        private ICommand _showYearCommand;

        public ICommand ShowYearCommand
        {
            get
            {
                if (_showYearCommand == null)
                    _showYearCommand = new DelegateCommand((e) => ShowYearAction(e));
                return _showYearCommand;
            }
            private set
            {
                _showYearCommand = value;
            }
        }

        private ICommand _incomeCommand;

        public ICommand IncomeCommand
        {
            get
            {
                if (_incomeCommand == null)
                    _incomeCommand = new DelegateCommand((e) => IncomeAction(e));
                return _incomeCommand;
            }
            private set
            {
                _incomeCommand = value;
            }
        }

        private ICommand _expenceCommand;

        public ICommand ExpenceCommand
        {
            get
            {
                if (_expenceCommand == null)
                    _expenceCommand = new DelegateCommand((e) => ExpenceAction(e));
                return _expenceCommand;
            }
            private set
            {
                _expenceCommand = value;
            }
        }

        #endregion

        #region Command Actions

        private void ShowYearAction(object e)
        {
            MonthVisible = !MonthVisible;
            OnPropertyChanged("Period");
            ShowYearCommandContent = MonthVisible ? "Show data per year" : "Show data per month";
            MoneyBox = InitMoneyBox();
        }

        private void IncomeAction(object e)
        {
            _navigationService.Navigate(typeof(CreateTransactionPage));
        }

        private void ExpenceAction(object e)
        {
            _navigationService.Navigate(typeof(CreateTransactionPage));
        }

        #endregion

        #region Properties

        private MoneyBoxWithBars _moneyBox;

        public MoneyBoxWithBars MoneyBox
        {
            get
            {
                if (_moneyBox == null)
                    _moneyBox = InitMoneyBox();

                return _moneyBox;
            }
            set
            {
                _moneyBox = value;
                OnPropertyChanged();
            }
        }

        private DateTime _period = DateTime.Now;

        public DateTime Period
        {
            get
            {
                return _period;
            }
            set
            {
                _period = value;
                OnPropertyChanged();
                MoneyBox = InitMoneyBox();
            }
        }

        private bool _monthVisible = true;

        public bool MonthVisible
        {
            get
            {
                return _monthVisible;
            }
            set
            {
                _monthVisible = value;
                OnPropertyChanged();
            }
        }

        private string _showYearCommandContent = "Show data per year";

        public string ShowYearCommandContent {
            get
            {
                return _showYearCommandContent;
            }
            set
            {
                _showYearCommandContent = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Platform Events

        private void BackButtonPressed(object sender, EventArgs e)
        {
            _navigationService.Navigate(typeof(MoneyBoxesPage));

            _platformEvents.BackButtonPressed -= BackButtonPressed;
        }

        #endregion

        #region Helping Methods

        private MoneyBoxWithBars InitMoneyBox()
        {
            object moneyBoxId = _navigationService.GetNavigationData("MoneyBoxId");

            if (moneyBoxId != null)
            {
                MoneyBox moneyBox = _moneyBoxService.Get((int)moneyBoxId);

                MoneyBoxWithBars newMoneyBox = new MoneyBoxWithBars()
                {
                    Id = moneyBox.Id,
                    Income = _reportingPeriodService.GetIncomeForSpecPeriod(moneyBox.Id, Period, MonthVisible),
                    Expence = _reportingPeriodService.GetExpenceForSpecPeriod(moneyBox.Id, Period, MonthVisible),
                    Balance = _reportingPeriodService.GetBalanceForSpecPeriod(moneyBox.Id, Period, MonthVisible),
                    Budget = _reportingPeriodService.GetBudgetForSpecPeriod(moneyBox.Id, Period, MonthVisible),
                    Available = _reportingPeriodService.GetBudgetBalanceForSpecPeriod(moneyBox.Id, Period, MonthVisible),
                    Image = moneyBox.PrimaryCurrency.Image,
                    Title = moneyBox.Title,
                    CurrencySymbol = moneyBox.PrimaryCurrency.Symbol
                };

                return newMoneyBox;
            }
            else
                return new MoneyBoxWithBars();
        }

        #endregion
    }
}
