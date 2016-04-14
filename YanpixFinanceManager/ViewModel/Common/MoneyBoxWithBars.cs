using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YanpixFinanceManager.Model.Entities;

namespace YanpixFinanceManager.ViewModel.Common
{
    public class MoneyBoxWithBars : BindableBase
    {
        private decimal _maxValue;

        private int _id { get; set; }

        public int Id
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
                OnPropertyChanged();
            }
        }

        private string _image;

        public string Image
        {
            get
            {
                return _image;
            }
            set
            {
                _image = value;
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

        private decimal _income;

        public decimal Income
        {
            get
            {
                return _income;
            }
            set
            {
                _income = value;
                OnPropertyChanged();
            }
        }

        private decimal _expence;

        public decimal Expence
        {
            get
            {
                return _expence;
            }
            set
            {
                _expence = value;
                OnPropertyChanged();
            }
        }

        private decimal _balance;

        public decimal Balance
        {
            get
            {
                return _balance;
            }
            set
            {
                _balance = value;
                OnPropertyChanged();
            }
        }

        private decimal _budget;

        public decimal Budget
        {
            get
            {
                return _budget;
            }
            set
            {
                _budget = value;
                OnPropertyChanged();
            }
        }

        private decimal _available;

        public decimal Available
        {
            get
            {
                return _available;
            }
            set
            {
                _available = value;
                OnPropertyChanged();
                _maxValue = MaxValue();
                InitScaleFactors();
            }
        }

        #region Bars Properties

        private double _incomeScaleFactor;

        public double IncomeScaleFactor
        {
            get
            {
                return _incomeScaleFactor;
            }
            set
            {
                _incomeScaleFactor = value;
                OnPropertyChanged();
            }
        }

        private double _expenceScaleFactor ;

        public double ExpenceScaleFactor
        {
            get
            {
                return _expenceScaleFactor;
            }
            set
            {
                _expenceScaleFactor = value;
                OnPropertyChanged();
            }
        }

        private double _balanceScaleFactor;

        public double BalanceScaleFactor
        {
            get
            {
                return _balanceScaleFactor;
            }
            set
            {
                _balanceScaleFactor = value;
                OnPropertyChanged();
            }
        }

        private double _budgetScaleFactor;

        public double BudgetScaleFactor
        {
            get
            {
                return _budgetScaleFactor;
            }
            set
            {
                _budgetScaleFactor = value;
                OnPropertyChanged();
            }
        }

        private double _availableScaleFactor;

        public double AvailableScaleFactor
        {
            get
            {
                return _availableScaleFactor;
            }
            set
            {
                _availableScaleFactor = value;
                OnPropertyChanged();
            }
        }

        private double _balanceMarginScaleFactor;

        public double BalanceMarginScaleFactor
        {
            get
            {
                return _balanceMarginScaleFactor;
            }
            set
            {
                _balanceMarginScaleFactor = value;
                OnPropertyChanged();
            }
        }

        private double _availableMarginScaleFactor;

        public double AvailableMarginScaleFactor
        {
            get
            {
                return _availableMarginScaleFactor;
            }
            set
            {
                _availableMarginScaleFactor = value;
                OnPropertyChanged();
            }
        }

        private bool _isBalancePositive;

        public bool IsBalancePositive
        {
            get
            {
                return _isBalancePositive;
            }
            set
            {
                _isBalancePositive = value;
                OnPropertyChanged();
            }
        }

        private bool _isAvailablePositive;

        public bool IsAvailablePositive
        {
            get
            {
                return _isAvailablePositive;
            }
            set
            {
                _isAvailablePositive = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Helping Methods

        private decimal MaxValue()
        {
            return Math.Max(Income, Math.Max(Expence, Budget));
        }

        private void InitScaleFactors()
        {
            if (!Income.Equals(0.0M) || !Expence.Equals(0.0M) || !Budget.Equals(0.0M))
            {
                _incomeScaleFactor = Math.Round(Income.Equals(_maxValue) ? 1.0 : (double)(Income / _maxValue), 2);
                _expenceScaleFactor = Math.Round(Expence.Equals(_maxValue) ? 1.0 : (double)(Expence / _maxValue), 2);
                _budgetScaleFactor = Math.Round(Budget.Equals(_maxValue) ? 1.0 : (double)(Budget / _maxValue), 2);
                _balanceScaleFactor = Math.Round((double)(Math.Abs(Income - Expence) / _maxValue), 2);
                _availableScaleFactor = Math.Round((double)(Math.Abs(Budget - Expence) / _maxValue), 2);

                _balanceMarginScaleFactor = Math.Round(!Budget.Equals(_maxValue) ? 0.0 : (double)(Math.Abs(Budget - Math.Max(Income, Expence)) / _maxValue), 2);
                _availableMarginScaleFactor = Math.Round(Budget > Expence ? (double)(Math.Abs(_maxValue - Budget) / _maxValue) : (double)(Math.Abs(_maxValue - Expence) / _maxValue), 2);

                _isBalancePositive = Income > Expence;
                _isAvailablePositive = Budget > Expence;
            }
        }

        #endregion
    }
}
