using FinanceManager.Common;
using FinanceManager.Infrastructure;
using FinanceManager.Model.DataAccess.Providers;
using FinanceManager.Model.DataAccess.Services;
using FinanceManager.Model.Entities;
using FinanceManager.Model.Entities.Enums;
using FinanceManager.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml;

namespace FinanceManager.ViewModel
{
    public class MoneyBoxViewModel : BaseViewModel
    {
        public MoneyBoxViewModel()
        {
            navigationData = new Dictionary<string, object>();
            _selectedPivotItemInit = false;

            EditMoneyBoxCommand = new RelayCommand(EditMoneyBox);
            AddIncomeCommand = new RelayCommand(AddIncome);
            AddExpenceCommand = new RelayCommand(AddExpence);
            DeleteAllTransactionsCommand = new RelayCommand(DeleteAllTransactions);
        }

        Dictionary<string, object> navigationData;

        #region Services

        public INavigationService NavigationService { get; set; }

        public IDataServicesProvider DataService { get; set; }

        #endregion

        #region Commands

        public ICommand EditMoneyBoxCommand { get; private set; }

        public ICommand AddIncomeCommand { get; private set; }

        public ICommand AddExpenceCommand { get; private set; }

        public ICommand DeleteAllTransactionsCommand { get; private set; }

        #endregion

        #region Commands implementation

        private void EditMoneyBox()
        {
            ;
        }

        private void AddIncome()
        {
            navigationData.Add("MoneyBoxId", MoneyBox.Id);
            navigationData.Add("TransactionType", TransactionType.Income);
            navigationData.Add("SelectedPivotItemOnMoneyBoxPage", SelectedPivotItem);

            NavigationService.Navigate(typeof(CreateTransactionPage), navigationData);
        }

        private void AddExpence()
        {
            navigationData.Add("MoneyBoxId", MoneyBox.Id);
            navigationData.Add("TransactionType", TransactionType.Expence);
            navigationData.Add("SelectedPivotItemOnMoneyBoxPage", SelectedPivotItem);

            NavigationService.Navigate(typeof(CreateTransactionPage), navigationData);
        }

        private void DeleteAllTransactions()
        {
            ;
        }

        #endregion

        #region Properties

        private MoneyBox _moneyBox;

        public MoneyBox MoneyBox
        {
            get
            {
                if (_moneyBox == null)
                    _moneyBox = LoadMoneyBox();
                return _moneyBox;
            }
            set
            {
                _moneyBox = value;
                OnPropertyChanged();
            }
        }

        private decimal _totalIncome;

        public decimal TotalIncome
        {
            get
            {
                _totalIncome = CalculateTotalIncome();
                return _totalIncome;
            }
            set
            {
                _totalIncome = value;
                OnPropertyChanged();
            }
        }

        private decimal _totalExpence;

        public decimal TotalExpence
        {
            get
            {
                _totalExpence = CalculateTotalExpence();
                return _totalExpence;
            }
            set
            {
                _totalExpence = value;
                OnPropertyChanged();
            }
        }

        private List<Transaction> _incomeTransactions;

        public List<Transaction> IncomeTransactions
        {
            get
            {
                if (_incomeTransactions == null)
                    _incomeTransactions = LoadIncomeTransactions();
                return _incomeTransactions;
            }
            set
            {
                _incomeTransactions = value;
                OnPropertyChanged();
            }
        }

        private List<Transaction> _expenceTransactions;

        public List<Transaction> ExpenceTransactions
        {
            get
            {
                if (_expenceTransactions == null)
                    _expenceTransactions = LoadExpenceTransactions();
                return _expenceTransactions;
            }
            set
            {
                _expenceTransactions = value;
                OnPropertyChanged();
            }
        }

        private Transaction _selectedTransaction;

        public Transaction SelectedTransaction
        {
            get
            {
                return _selectedTransaction;
            }
            set
            {
                _selectedTransaction = value;
                OnPropertyChanged();
                GoToSelectedTransaction();
            }
        }

        private List<TransactionByCategory> _incomesByCategory;

        public List<TransactionByCategory> IncomesByCategory
        {
            get
            {
                if (_incomesByCategory == null)
                    _incomesByCategory = LoadIncomesByCategory();
                return _incomesByCategory;
            }
            set
            {
                _incomesByCategory = value;
                OnPropertyChanged();
            }
        }

        private List<TransactionByCategory> _expencesByCategory;

        public List<TransactionByCategory> ExpencesByCategory
        {
            get
            {
                if (_expencesByCategory == null)
                    _expencesByCategory = LoadExpencesByCategory();
                return _expencesByCategory;
            }
            set
            {
                _expencesByCategory = value;
                OnPropertyChanged();
            }
        }

        private bool _selectedPivotItemInit;

        private int _selectedPivotItem;

        public int SelectedPivotItem
        {
            get
            {
                if (!_selectedPivotItemInit)
                {
                    _selectedPivotItem = LoadSelectedPivotItem(_selectedPivotItem);
                    _selectedPivotItemInit = true;
                    SwitchCommandBarsVisibility();
                }
                return _selectedPivotItem;
            }
            set
            {
                _selectedPivotItem = value;
                OnPropertyChanged();
                SwitchCommandBarsVisibility();
            }
        }

        private bool _moneyBoxCommandBarVisible;

        public bool MoneyBoxCommandBarVisible
        {
            get
            {
                return _moneyBoxCommandBarVisible;
            }
            set
            {
                _moneyBoxCommandBarVisible = value;
                OnPropertyChanged();
            }
        }

        private bool _transactionsCommandBarVisible;

        public bool TransactionsCommandBarVisible
        {
            get
            {
                return _transactionsCommandBarVisible;
            }
            set
            {
                _transactionsCommandBarVisible = value;
                OnPropertyChanged();
            }
        }

        private bool _commandBarVisible;

        public bool CommandBarVisible
        {
            get
            {
                return _commandBarVisible;
            }
            set
            {
                _commandBarVisible = value;
                OnPropertyChanged();
            }
        }

        private string _mostProfitableCategory;

        public string MostProfitableCategory
        {
            get
            {
                _mostProfitableCategory = GetMostProfitableCategory();
                return _mostProfitableCategory;
            }
            set
            {
                _mostProfitableCategory = value;
                OnPropertyChanged();
            }
        }

        private string _mostUnprofitableCategory;

        public string MostUnprofitableCategory
        {
            get
            {
                _mostUnprofitableCategory = GetMostUnprofitableCategory();
                return _mostUnprofitableCategory;
            }
            set
            {
                _mostUnprofitableCategory = value;
                OnPropertyChanged();
            }
        }

        private double _averageIncomePerDay;

        public double AverageIncomePerDay
        {
            get
            {
                _averageIncomePerDay = CalculateAverageIncomePerDay();
                return _averageIncomePerDay;
            }
            set
            {
                _averageIncomePerDay = value;
                OnPropertyChanged();
            }
        }

        private double _averageExpencePerDay;

        public double AverageExpencePerDay
        {
            get
            {
                _averageExpencePerDay = CalculateAverageExpencePerDay();
                return _averageExpencePerDay;
            }
            set
            {
                _averageExpencePerDay = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Helping methods

        private List<Transaction> LoadIncomeTransactions()
        {
            return DataService.Get<Transaction>()
                .GetAll()
                .Where(t => t.MoneyBoxId == MoneyBox.Id && t.Type == TransactionType.Income)
                .OrderByDescending(o => o.CreationDate)
                .ToList();
        }

        private List<Transaction> LoadExpenceTransactions()
        {
            return DataService.Get<Transaction>()
                .GetAll()
                .Where(t => t.MoneyBoxId == MoneyBox.Id && t.Type == TransactionType.Expence)
                .OrderByDescending(o => o.CreationDate)
                .ToList();
        }

        private MoneyBox LoadMoneyBox()
        {
            object data = NavigationService.GetNavigationData("MoneyBoxId");

            if (data != null)
                return DataService.Get<MoneyBox>().Get((int)data);
            else
                return null;
        }

        private decimal CalculateTotalIncome()
        {
            return DataService.Get<Transaction>()
                .GetAll()
                .Where(t => t.MoneyBoxId == MoneyBox.Id && t.Type == TransactionType.Income)
                .Select(t => t.Value)
                .Sum();
        }

        private decimal CalculateTotalExpence()
        {
            return DataService.Get<Transaction>()
                .GetAll()
                .Where(t => t.MoneyBoxId == MoneyBox.Id && t.Type == TransactionType.Expence)
                .Select(t => t.Value)
                .Sum();
        }

        private void GoToSelectedTransaction()
        {
            ;
        }

        private List<TransactionByCategory> LoadIncomesByCategory()
        {
            return DataService.Get<Transaction>()
                .GetAll()
                .Where(t => t.MoneyBoxId == MoneyBox.Id && t.Type == TransactionType.Income)
                .GroupBy(g => g.Category.Title)
                .Select(s => new TransactionByCategory
                {
                    CategoryTitle = s.Key,
                    TransactionValue = (double)s.Select(x => x.Value).Sum()
                })
                .OrderByDescending(o => o.TransactionValue)
                .ToList();
        }

        private List<TransactionByCategory> LoadExpencesByCategory()
        {
            return DataService.Get<Transaction>()
                .GetAll()
                .Where(t => t.MoneyBoxId == MoneyBox.Id && t.Type == TransactionType.Expence)
                .GroupBy(g => g.Category.Title)
                .Select(s => new TransactionByCategory
                {
                    CategoryTitle = s.Key,
                    TransactionValue = (double)s.Select(x => x.Value).Sum()
                })
                .OrderByDescending(o => o.TransactionValue)
                .ToList();
        }

        private void SwitchCommandBarsVisibility()
        {
            switch (_selectedPivotItem)
            {
                case 0:
                    CommandBarVisible = true;
                    MoneyBoxCommandBarVisible = true;
                    TransactionsCommandBarVisible = false;
                    break;
                case 1:
                    CommandBarVisible = true;
                    MoneyBoxCommandBarVisible = false;
                    TransactionsCommandBarVisible = true;
                    break;
                case 2:
                    CommandBarVisible = false;
                    MoneyBoxCommandBarVisible = false;
                    TransactionsCommandBarVisible = false;
                    break;
                default:
                    break;
            }
        }

        private string GetMostProfitableCategory()
        {
            if (IncomesByCategory.Count > 0)
            {
                return IncomesByCategory
                .OrderBy(o => o.TransactionValue)
                .LastOrDefault()
                .CategoryTitle;
            }
            else
                return "";
        }

        private string GetMostUnprofitableCategory()
        {
            if (ExpencesByCategory.Count > 0)
            {
                return ExpencesByCategory
                .OrderBy(o => o.TransactionValue)
                .LastOrDefault()
                .CategoryTitle;
            }
            else
                return "";
        }

        private double CalculateAverageIncomePerDay()
        {
            double total = IncomesByCategory.Sum(s => s.TransactionValue);
            int days = DataService.Get<Transaction>()
                .GetAll()
                .Select(s => s.CreationDate.Date)
                .Distinct()
                .Count();
            if (days > 0)
            {
                return total / days;
            }
            else
                return 0;
        }

        private double CalculateAverageExpencePerDay()
        {
            double total = ExpencesByCategory.Sum(s => s.TransactionValue);
            int days = DataService.Get<Transaction>()
                .GetAll()
                .Select(s => s.CreationDate.Date)
                .Distinct()
                .Count();
            if (days > 0)
            {
                return total / days;
            }
            else
                return 0;
        }

        private int LoadSelectedPivotItem(int currentPivotItem)
        {
            object selectedPivotItem = NavigationService.GetNavigationData("SelectedPivotItemOnMoneyBoxPage");

            if (selectedPivotItem != null && currentPivotItem != (int)selectedPivotItem)
                return (int)selectedPivotItem;
            else
                return currentPivotItem;
        }

        #endregion
    }

    public class TransactionByCategory
    {
        public string CategoryTitle { get; set; }
        public double TransactionValue { get; set; }
    }
}
