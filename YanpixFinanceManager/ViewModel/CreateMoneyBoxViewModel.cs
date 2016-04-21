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

        private bool _isEdit = false;

        private MoneyBox _moneyBox = null;

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

            InitIfEdit();
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

        private ICommand _cancelCommand;

        public ICommand CancelCommand
        {
            get
            {
                if (_cancelCommand == null)
                    _cancelCommand = new DelegateCommand((e) => CancelAction(e));
                return _cancelCommand;
            }
            private set
            {
                _cancelCommand = value;
            }
        }

        #endregion

        #region Command Actions

        private void SaveAction(object e)
        {
            Validate();

            if (IsValid)
            {
                if (!_isEdit)
                {
                    MoneyBox newMoneyBox = new MoneyBox()
                    {
                        Title = Title,
                        YearBudget = YearBudget,
                        MonthBudget = MonthBudget
                    };

                    _moneyBoxService.Insert(newMoneyBox, false, false);

                    newMoneyBox.PrimaryCurrency = _currenciesService.Get(SelectedPrimaryCurrency.Id);

                    _moneyBoxService.Update(newMoneyBox, true);

                    ReportingPeriod yearPeriod = new ReportingPeriod()
                    {
                        Type = ReportingPeriodType.Year,
                        Period = DateTime.Now,
                        Budget = newMoneyBox.YearBudget,
                    };

                    _reportingPeriodsService.Insert(yearPeriod, false, false);

                    yearPeriod.MoneyBox = _moneyBoxService.Get(newMoneyBox.Id);

                    yearPeriod.ParentPeriod = null;

                    _reportingPeriodsService.Update(yearPeriod, true);

                    ReportingPeriod monthPeriod = new ReportingPeriod()
                    {
                        Type = ReportingPeriodType.Month,
                        Period = DateTime.Now,
                        Budget = newMoneyBox.MonthBudget,
                    };

                    _reportingPeriodsService.Insert(monthPeriod, false, false);

                    monthPeriod.MoneyBox = _moneyBoxService.Get(newMoneyBox.Id);

                    monthPeriod.ParentPeriod = _reportingPeriodsService.Get(yearPeriod.Id);

                    _reportingPeriodsService.Update(monthPeriod, true);
                }
                else
                {
                    _moneyBox.Title = Title;
                    _moneyBox.YearBudget = YearBudget;
                    _moneyBox.MonthBudget = MonthBudget;

                    _moneyBoxService.Insert(_moneyBox, true, false);

                    _moneyBox.PrimaryCurrency = _currenciesService.Get(SelectedPrimaryCurrency.Id);

                    _moneyBoxService.Update(_moneyBox, true);
                }

                _navigationService.ClearNavigationData("MoneyBoxId");
                _navigationService.Navigate(typeof(MoneyBoxesPage));
            }
        }

        private void CancelAction(object e)
        {
            _navigationService.ClearNavigationData("MoneyBoxId");
            _navigationService.Navigate(typeof(MoneyBoxesPage));
        }

        #endregion

        #region Properties

        private string _pageTitle;

        public string PageTitle
        {
            get
            {
                return _pageTitle;
            }
            set
            {
                _pageTitle = value;
                OnPropertyChanged();
            }
        }

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
            }
        }

        private decimal _yearBudget;

        public decimal YearBudget
        {
            get
            {
                return _yearBudget;
            }
            set
            {
                _yearBudget = value;
                OnPropertyChanged();
            }
        }

        private decimal _monthBudget;

        public decimal MonthBudget
        {
            get
            {
                return _monthBudget;
            }
            set
            {
                _monthBudget = value;
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
                existingMoneyBox = _moneyBoxService.ExistingMoneyBox(Title, SelectedPrimaryCurrency.Id);

            if (string.IsNullOrEmpty(Title))
                ValidationErrors["Title"] = "Title is required";
            else if (Title.Length > 50)
                ValidationErrors["Title"] = "Title must contain up to 50 characters";
            else if (existingMoneyBox != null && !_isEdit)
                ValidationErrors["Title"] = "Money Box already exists";
            
            if (SelectedPrimaryCurrency == null)
                ValidationErrors["PrimaryCurrency"] = "Primary Currency is required";

            if (YearBudget.Equals(0.0M))
                ValidationErrors["YearBudget"] = "Year Budget is required";

            if (MonthBudget.Equals(0.0M))
                ValidationErrors["MonthBudget"] = "Month Budget is required";

            IsValid = ValidationErrors.IsValid;

            OnPropertyChanged("ValidationErrors");
        }

        #endregion

        #region Helping Methods

        private void InitIfEdit()
        {
            object moneyBoxId = _navigationService.GetNavigationData("MoneyBoxId");

            if (moneyBoxId != null)
            {
                _isEdit = true;

                _moneyBox = _moneyBoxService.Get((int)moneyBoxId);

                PageTitle = "Edit Box";
                Title = _moneyBox.Title;
                YearBudget = _moneyBox.YearBudget;
                MonthBudget = _moneyBox.MonthBudget;
                SelectedPrimaryCurrency = Currencies.Where(x => x.Id == _moneyBox.PrimaryCurrency.Id).Single();
            }
            else
            {
                PageTitle = "New Box";
                Title = "";
                YearBudget = 0M;
                MonthBudget = 0M;
            }
        }

        #endregion

        #region Platform Events

        private void BackButtonPressed(object sender, EventArgs e)
        {
            _navigationService.ClearNavigationData("MoneyBoxId");

            _navigationService.Navigate(typeof(MoneyBoxesPage));

            _platformEvents.BackButtonPressed -= BackButtonPressed;
        }

        #endregion
    }
}
