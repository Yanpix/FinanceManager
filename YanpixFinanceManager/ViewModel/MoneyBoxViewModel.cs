using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

                return new MoneyBoxWithBars()
                {
                    Id = moneyBox.Id,
                    Income = _reportingPeriodService.GetIncomeForLastMonth(moneyBox.Id),
                    Expence = _reportingPeriodService.GetExpenceForLastMonth(moneyBox.Id),
                    Balance = _reportingPeriodService.GetBalanceForLastMonth(moneyBox.Id),
                    Budget = _reportingPeriodService.GetBudgetForLastMonth(moneyBox.Id),
                    Available = _reportingPeriodService.GetBudgetBalanceForLastMonth(moneyBox.Id),
                    Image = moneyBox.PrimaryCurrency.Image,
                    Title = moneyBox.Title
                };
            }
            else
                return new MoneyBoxWithBars();
        }

        #endregion
    }
}
