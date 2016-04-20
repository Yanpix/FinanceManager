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
    public class CreateMoneyBoxViewModel : BindableBase
    {
        #region Fields & Constructor

        private INavigationService _navigationService;

        private IEntityBaseService<MoneyBox> _moneyBoxService;

        private IEntityBaseService<Currency> _currenciesService;

        private IEntityBaseService<ReportingPeriod> _reportingPeriodsService;

        private IPlatformEvents _platformEvents;

        private Dictionary<string, object> _navigationData;

        public CreateMoneyBoxViewModel(INavigationService navigationService,
            IEntityBaseService<MoneyBox> moneyBoxService,
            IEntityBaseService<Currency> currenciesService,
            IEntityBaseService<ReportingPeriod> reportingPeriodsService,
            IPlatformEvents platformEvents)
        {
            _navigationService = navigationService;
            _moneyBoxService = moneyBoxService;
            _currenciesService = currenciesService;
            _reportingPeriodsService = reportingPeriodsService;
            _platformEvents = platformEvents;
            _navigationData = new Dictionary<string, object>();

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
            Validate();

            if (IsValid)
            {
                _moneyBoxService.Insert(MoneyBox, false, false);

                MoneyBox.PrimaryCurrency = _currenciesService.Get(SelectedPrimaryCurrency.Id);

                _moneyBoxService.Update(MoneyBox, true);

                ReportingPeriod yearPeriod = new ReportingPeriod()
                {
                    Type = ReportingPeriodType.Year,
                    Period = DateTime.Now,
                    Budget = MoneyBox.YearBudget,
                };

                _reportingPeriodsService.Insert(yearPeriod, false, false);

                yearPeriod.MoneyBox = _moneyBoxService.Get(MoneyBox.Id);

                yearPeriod.ParentPeriod = null;

                _reportingPeriodsService.Update(yearPeriod, true);

                ReportingPeriod monthPeriod = new ReportingPeriod()
                {
                    Type = ReportingPeriodType.Month,
                    Period = DateTime.Now,
                    Budget = MoneyBox.MonthBudget,
                };

                _reportingPeriodsService.Insert(monthPeriod, false, false);

                monthPeriod.MoneyBox = _moneyBoxService.Get(MoneyBox.Id);

                monthPeriod.ParentPeriod = _reportingPeriodsService.Get(yearPeriod.Id);

                _reportingPeriodsService.Update(monthPeriod, true);

                _navigationService.Navigate(typeof(MoneyBoxesPage));
            }
        }

        #endregion

        #region Properties

        private MoneyBox _moneyBox;

        public MoneyBox MoneyBox
        {
            get
            {
                if (_moneyBox == null)
                    _moneyBox = new MoneyBox();

                return _moneyBox;
            }
            set
            {
                _moneyBox = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Currency> _currencies;

        public ObservableCollection<Currency> Currencies
        {
            get
            {
                if (_currencies == null)
                    _currencies = new ObservableCollection<Currency>(_currenciesService.GetAll());

                return _currencies;
            }
            set
            {
                _currencies = value;
                OnPropertyChanged();
            }
        }

        private Currency _selectedPrimaryCurrency;

        public Currency SelectedPrimaryCurrency
        {
            get
            {
                return _selectedPrimaryCurrency;
            }
            set
            {
                _selectedPrimaryCurrency = value;
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

            MoneyBox existingMoneyBox = null;

            if (SelectedPrimaryCurrency != null)
                existingMoneyBox = _moneyBoxService.ExistingMoneyBox(MoneyBox.Title, SelectedPrimaryCurrency.Id);

            if (string.IsNullOrEmpty(MoneyBox.Title))
                ValidationErrors["Title"] = "Title is required";
            else if (MoneyBox.Title.Length > 50)
                ValidationErrors["Title"] = "Title must contain up to 50 characters";
            else if (existingMoneyBox != null)
                ValidationErrors["Title"] = "Money Box already exists";
            
            if (SelectedPrimaryCurrency == null)
                ValidationErrors["PrimaryCurrency"] = "Primary Currency is required";

            if (MoneyBox.YearBudget.Equals(0.0M))
                ValidationErrors["YearBudget"] = "Year Budget is required";

            if (MoneyBox.MonthBudget.Equals(0.0M))
                ValidationErrors["MonthBudget"] = "Month Budget is required";

            IsValid = ValidationErrors.IsValid;

            OnPropertyChanged("ValidationErrors");
        }

        #endregion

        #region Platform Events

        private void BackButtonPressed(object sender, EventArgs e)
        {
            _navigationService.Navigate(typeof(MoneyBoxesPage));

            _platformEvents.BackButtonPressed -= BackButtonPressed;
        }

        #endregion
    }
}
