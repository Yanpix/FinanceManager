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
    public class MoneyBoxViewModel : BindableBase
    {
        #region Fields & Constructor

        private INavigationService _navigationService;

        private IEntityBaseService<ReportingPeriod> _reportingPeriodService;

        private IEntityBaseService<MoneyBox> _moneyBoxService;

        private IEntityBaseService<Transaction> _transactionsService;

        private IEntityBaseService<Category> _categoriesService;

        private IPlatformEvents _platformEvents;

        private Dictionary<string, object> _navigationData;

        public MoneyBoxViewModel(INavigationService navigationService,
            IEntityBaseService<MoneyBox> moneyBoxService,
            IEntityBaseService<ReportingPeriod> reportingPeriodService,
            IEntityBaseService<Transaction> transactionsService,
            IEntityBaseService<Category> categoriesService,
            IPlatformEvents platformEvents)
        {
            _navigationService = navigationService;
            _moneyBoxService = moneyBoxService;
            _reportingPeriodService = reportingPeriodService;
            _transactionsService = transactionsService;
            _categoriesService = categoriesService;
            _platformEvents = platformEvents;
            _navigationData = new Dictionary<string, object>();

            _platformEvents.BackButtonPressed += BackButtonPressed;

            SelectedPivot = 0;
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

        private ICommand _deleteMoneyBoxCommand;

        public ICommand DeleteMoneyBoxCommand
        {
            get
            {
                if (_deleteMoneyBoxCommand == null)
                    _deleteMoneyBoxCommand = new DelegateCommand((e) => DeleteMoneyBoxAction(e));
                return _deleteMoneyBoxCommand;
            }
            private set
            {
                _deleteMoneyBoxCommand = value;
            }
        }

        private ICommand _filterAllCommand;

        public ICommand FilterAllCommand
        {
            get
            {
                if (_filterAllCommand == null)
                    _filterAllCommand = new DelegateCommand((e) => FilterAllAction(e));
                return _filterAllCommand;
            }
            private set
            {
                _filterAllCommand = value;
            }
        }

        private ICommand _filterIncomesCommand;

        public ICommand FilterIncomesCommand
        {
            get
            {
                if (_filterIncomesCommand == null)
                    _filterIncomesCommand = new DelegateCommand((e) => FilterIncomesAction(e));
                return _filterIncomesCommand;
            }
            private set
            {
                _filterIncomesCommand = value;
            }
        }

        private ICommand _filterExpencesCommand;

        public ICommand FilterExpencesCommand
        {
            get
            {
                if (_filterExpencesCommand == null)
                    _filterExpencesCommand = new DelegateCommand((e) => FilterExpencesAction(e));
                return _filterExpencesCommand;
            }
            private set
            {
                _filterExpencesCommand = value;
            }
        }

        private ICommand _sortByDateCommand;

        public ICommand SortByDateCommand
        {
            get
            {
                if (_sortByDateCommand == null)
                    _sortByDateCommand = new DelegateCommand((e) => SortByDateAction(e));
                return _sortByDateCommand;
            }
            private set
            {
                _sortByDateCommand = value;
            }
        }

        private ICommand _sortByAmountCommand;

        public ICommand SortByAmountCommand
        {
            get
            {
                if (_sortByAmountCommand == null)
                    _sortByAmountCommand = new DelegateCommand((e) => SortByAmountAction(e));
                return _sortByAmountCommand;
            }
            private set
            {
                _sortByAmountCommand = value;
            }
        }

        private ICommand _sortByCategoryCommand;

        public ICommand SortByCategoryCommand
        {
            get
            {
                if (_sortByCategoryCommand == null)
                    _sortByCategoryCommand = new DelegateCommand((e) => SortByCategoryAction(e));
                return _sortByCategoryCommand;
            }
            private set
            {
                _sortByCategoryCommand = value;
            }
        }

        private ICommand _sortByCurrencyCommand;

        public ICommand SortByCurrencyCommand
        {
            get
            {
                if (_sortByCurrencyCommand == null)
                    _sortByCurrencyCommand = new DelegateCommand((e) => SortByCurrencyAction(e));
                return _sortByCurrencyCommand;
            }
            private set
            {
                _sortByCurrencyCommand = value;
            }
        }

        private ICommand _ascendingCommand;

        public ICommand AscendingCommand
        {
            get
            {
                if (_ascendingCommand == null)
                    _ascendingCommand = new DelegateCommand((e) => AscendingAction(e));
                return _ascendingCommand;
            }
            private set
            {
                _ascendingCommand = value;
            }
        }

        private ICommand _descendingCommand;

        public ICommand DescendingCommand
        {
            get
            {
                if (_descendingCommand == null)
                    _descendingCommand = new DelegateCommand((e) => DescendingAction(e));
                return _descendingCommand;
            }
            private set
            {
                _descendingCommand = value;
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
            _navigationData.Add("TransactionType", 0);
            _navigationData.Add("MoneyBoxId", MoneyBox.Id);
            _navigationService.Navigate(typeof(CreateTransactionPage), _navigationData);
        }

        private void ExpenceAction(object e)
        {
            _navigationData.Add("TransactionType", 1);
            _navigationData.Add("MoneyBoxId", MoneyBox.Id);
            _navigationService.Navigate(typeof(CreateTransactionPage), _navigationData);
        }

        private void DeleteMoneyBoxAction(object e)
        {
            _moneyBoxService.Delete(MoneyBox.Id);
            _navigationService.Navigate(typeof(MainPage), _navigationData);
        }

        private void FilterAllAction(object e)
        {
            FilterOption = "All";
        }

        private void FilterIncomesAction(object e)
        {
            FilterOption = "Incomes";
        }

        private void FilterExpencesAction(object e)
        {
            FilterOption = "Expences";
        }

        private void SortByDateAction(object e)
        {
            SortOption = "By Date";
        }

        private void SortByAmountAction(object e)
        {
            SortOption = "By Amount";
        }

        private void SortByCategoryAction(object e)
        {
            SortOption = "By Category";
        }

        private void SortByCurrencyAction(object e)
        {
            SortOption = "By Currency";
        }

        private void AscendingAction(object e)
        {
            SortOption = "Ascending";
        }

        private void DescendingAction(object e)
        {
            SortOption = "Descending";
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

        private ObservableCollection<Transaction> _transactions;

        public ObservableCollection<Transaction> Transactions
        {
            get
            {
                if (_transactions == null)
                    _transactions = FilterTransactions();
                return _transactions;
            }
            set
            {
                _transactions = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<CategoryWithBar> _categories;

        public ObservableCollection<CategoryWithBar> Categories
        {
            get
            {
                if (_categories == null)
                    _categories = InitCategories();
                return _categories;
            }
            set
            {
                _categories = value;
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
                Transactions = FilterTransactions();
                Categories = InitCategories();
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
                Transactions = FilterTransactions();
                Categories = InitCategories();
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

        private int _selectedPivot;

        public int SelectedPivot
        {
            get
            {
                return _selectedPivot;
            }
            set
            {
                _selectedPivot = value;
                OnPropertyChanged();
                MonthVisible = true;
                Period = DateTime.Now;
                switch (value)
                {
                    case 0:
                        FilterCommandsVisible = false;
                        SortCommandsVisible = false;
                        MoneyBoxCommandsVisible = true;
                        MoneyBoxSecondaryCommandsVisible = true;
                        break;
                    case 1:
                        FilterCommandsVisible = true;
                        SortCommandsVisible = true;
                        MoneyBoxCommandsVisible = true;
                        TransactionsSortCommandsVisible = true;
                        CategoriesSortCommandsVisible = false;
                        MoneyBoxSecondaryCommandsVisible = false;
                        FilterOption = "All";
                        SortOption = "By Date";
                        break;
                    case 2:
                        FilterCommandsVisible = true;
                        SortCommandsVisible = true;
                        TransactionsSortCommandsVisible = false;
                        CategoriesSortCommandsVisible = true;
                        MoneyBoxCommandsVisible = false;
                        MoneyBoxSecondaryCommandsVisible = false;
                        FilterOption = "All";
                        SortOption = "Descending";
                        break;
                }
            }
        }

        private bool _filterCommandsVisible = false;

        public bool FilterCommandsVisible
        {
            get
            {
                return _filterCommandsVisible;
            }
            set
            {
                _filterCommandsVisible = value;
                OnPropertyChanged();
            }
        }

        private bool _sortCommandsVisible = false;

        public bool SortCommandsVisible
        {
            get
            {
                return _sortCommandsVisible;
            }
            set
            {
                _sortCommandsVisible = value;
                OnPropertyChanged();
            }
        }

        private bool _moneyBoxCommandsVisible = false;

        public bool MoneyBoxCommandsVisible
        {
            get
            {
                return _moneyBoxCommandsVisible;
            }
            set
            {
                _moneyBoxCommandsVisible = value;
                OnPropertyChanged();
            }
        }

        private bool _moneyBoxSecondaryCommandsVisible = false;

        public bool MoneyBoxSecondaryCommandsVisible
        {
            get
            {
                return _moneyBoxSecondaryCommandsVisible;
            }
            set
            {
                _moneyBoxSecondaryCommandsVisible = value;
                OnPropertyChanged();
            }
        }

        private bool _transactionsSortCommandsVisible = false;

        public bool TransactionsSortCommandsVisible
        {
            get
            {
                return _transactionsSortCommandsVisible;
            }
            set
            {
                _transactionsSortCommandsVisible = value;
                OnPropertyChanged();
            }
        }

        private bool _categoriesSortCommandsVisible = false;

        public bool CategoriesSortCommandsVisible
        {
            get
            {
                return _categoriesSortCommandsVisible;
            }
            set
            {
                _categoriesSortCommandsVisible = value;
                OnPropertyChanged();
            }
        }

        private string _filterOption = "All";

        public string FilterOption
        {
            get
            {
                return _filterOption;
            }
            set
            {
                _filterOption = value;
                OnPropertyChanged();
                Transactions = FilterTransactions();
                Categories = InitCategories();
            }
        }

        private string _sortOption = "By Date";

        public string SortOption
        {
            get
            {
                return _sortOption;
            }
            set
            {
                _sortOption = value;
                OnPropertyChanged();
                Transactions = FilterTransactions();
                Categories = InitCategories();
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

        private ObservableCollection<CategoryWithBar> InitCategories()
        {
            var allCategories = _transactionsService.GetAll()
                .Where(x => x.ReportingPeriod.MoneyBoxId == MoneyBox.Id)
                .Select(s => new { s.Category.Image, s.Category.Title, s.Category.Type, s.ReportingPeriod })
                .Distinct()
                .ToList();



            if (MonthVisible)
                allCategories = allCategories
                    .Where(x => x.ReportingPeriod.Period.Month == Period.Month &&
                        x.ReportingPeriod.Period.Year == Period.Year)
                    .ToList();
            else
                allCategories = allCategories
                    .Where(x => x.ReportingPeriod.Period.Year == Period.Year)
                    .ToList();

            switch (FilterOption)
            {
                case "Incomes":
                    allCategories = allCategories.Where(x => x.Type == TransactionType.Income).ToList();
                    break;
                case "Expences":
                    allCategories = allCategories.Where(x => x.Type == TransactionType.Expence).ToList();
                    break;
            }

            var filteredCategories = new ObservableCollection<CategoryWithBar>(
                allCategories.Select(x => new CategoryWithBar()
                {
                    Image = x.Image,
                    CurrencySymbol = MoneyBox.CurrencySymbol,
                    Title = x.Title,
                    Type = x.Type,
                    Value = _transactionsService.GetAll()
                        .Where(c => c.ReportingPeriod.MoneyBoxId == MoneyBox.Id && c.Category.Title.Equals(x.Title) && c.Category.Type == x.Type)
                        .Sum(s => s.Value * s.ToPrimaryCoeff),
                    Total = x.Type == TransactionType.Income ?
                        _reportingPeriodService.GetIncomeForSpecPeriod(MoneyBox.Id, Period, MonthVisible) :
                        _reportingPeriodService.GetExpenceForSpecPeriod(MoneyBox.Id, Period, MonthVisible)
                }));

            switch (SortOption)
            {
                case "Ascending":
                    return new ObservableCollection<CategoryWithBar>(filteredCategories.OrderBy(o => o.Value));
                case "Descending":
                    return new ObservableCollection<CategoryWithBar>(filteredCategories.OrderByDescending(o => o.Value));
                default:
                    return new ObservableCollection<CategoryWithBar>(filteredCategories.OrderByDescending(o => o.Value));
            }
        }

        private ObservableCollection<Transaction> FilterTransactions()
        {
            List<Transaction> allTransactions = _transactionsService.GetAll()
                .Where(x => x.ReportingPeriod.MoneyBoxId == MoneyBox.Id).ToList();

            List<Transaction> filteredTransactions = new List<Transaction>();

            if (MonthVisible)
                filteredTransactions = allTransactions
                    .Where(x => x.ReportingPeriod.Period.Month == Period.Month && 
                        x.ReportingPeriod.Period.Year == Period.Year)
                    .ToList();
            else
                filteredTransactions = allTransactions
                    .Where(x => x.ReportingPeriod.Period.Year == Period.Year)
                    .ToList();

            switch (FilterOption)
            {
                case "Incomes":
                    filteredTransactions = filteredTransactions.Where(x => x.Type == TransactionType.Income).ToList();
                    break;
                case "Expences":
                    filteredTransactions = filteredTransactions.Where(x => x.Type == TransactionType.Expence).ToList();
                    break;
            }

            switch (SortOption)
            {
                case "By Date":
                    return new ObservableCollection<Transaction>(filteredTransactions.OrderByDescending(o => o.Created));
                case "By Amount":
                    return new ObservableCollection<Transaction>(filteredTransactions.OrderByDescending(o => o.Value));
                case "By Category":
                    return new ObservableCollection<Transaction>(filteredTransactions.OrderByDescending(o => o.Category.Title));
                case "By Currency":
                    return new ObservableCollection<Transaction>(filteredTransactions.OrderByDescending(o => o.Currency.Title));
                default:
                    return new ObservableCollection<Transaction>(filteredTransactions.OrderByDescending(o => o.Created));
            }
        }

        #endregion
    }
}
