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
            EditMoneyBoxCommand = new RelayCommand(EditMoneyBox);
            AddIncomeCommand = new RelayCommand(AddIncome);
            AddExpenceCommand = new RelayCommand(AddExpence);
            DeleteAllTransactionsCommand = new RelayCommand(DeleteAllTransactions);
        }

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
            NavigationService.Navigate(typeof(CreateTransactionPage), 
                new object[] { MoneyBox.Id, TransactionType.Income });
        }

        private void AddExpence()
        {
            NavigationService.Navigate(typeof(CreateTransactionPage), 
                new object[] { MoneyBox.Id, TransactionType.Expence });
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
                return _totalIncome;
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
            return DataService.Get<MoneyBox>().Get((int)NavigationService.GetNavigationData(0));
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
                .GroupBy(g => g.Category)
                .Select(s => new TransactionByCategory
                {
                    CategoryTitle = s.Key.Title,
                    TransactionValue = (double)s.Sum(c => c.Value)
                })
                .OrderBy(o => o.TransactionValue)
                .ToList();
        }

        private List<TransactionByCategory> LoadExpencesByCategory()
        {
            return DataService.Get<Transaction>()
                .GetAll()
                .Where(t => t.MoneyBoxId == MoneyBox.Id && t.Type == TransactionType.Expence)
                .GroupBy(g => g.Category)
                .Select(s => new TransactionByCategory
                {
                    CategoryTitle = s.Key.Title,
                    TransactionValue = (double)s.Sum(c => c.Value)
                })
                .OrderBy(o => o.TransactionValue)
                .ToList();
        }

        #endregion
    }

    public class TransactionByCategory
    {
        public string CategoryTitle { get; set; }
        public double TransactionValue { get; set; }
    }
}
