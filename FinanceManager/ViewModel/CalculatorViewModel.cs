using FinanceManager.Common;
using FinanceManager.Infrastructure;
using FinanceManager.View;
using Jace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FinanceManager.ViewModel
{
    public class CalculatorViewModel : BaseViewModel
    {
        // View model constructor
        public CalculatorViewModel()
        {
            navigationData = new Dictionary<string, object>();
            Expression = Expression.Clear();

            // Initialize commands
            AcceptCommand = new RelayCommand(Accept);
            CancelCommand = new RelayCommand(Cancel);
            SetToZeroCommand = new RelayCommand(SetToZero);
            DeleteCommand = new RelayCommand(Delete);
            DivideCommand = new RelayCommand(Divide);
            MultiplyCommand = new RelayCommand(Multiply);
            Number7Command = new RelayCommand(Number7);
            Number8Command = new RelayCommand(Number8);
            Number9Command = new RelayCommand(Number9);
            SubtractCommand = new RelayCommand(Subtract);
            Number4Command = new RelayCommand(Number4);
            Number5Command = new RelayCommand(Number5);
            Number6Command = new RelayCommand(Number6);
            AddCommand = new RelayCommand(Add);
            Number1Command = new RelayCommand(Number1);
            Number2Command = new RelayCommand(Number2);
            Number3Command = new RelayCommand(Number3);
            Number0Command = new RelayCommand(Number0);
            DotCommand = new RelayCommand(Dot);
            EquelCommand = new RelayCommand(Equel);
        }

        Dictionary<string, object> navigationData;

        #region Services

        public INavigationService NavigationService { get; set; }

        #endregion

        #region Commands

        public ICommand AcceptCommand { get; private set; }

        public ICommand CancelCommand { get; private set; }

        public ICommand SetToZeroCommand { get; private set; }

        public ICommand DeleteCommand { get; private set; }

        public ICommand DivideCommand { get; private set; }

        public ICommand MultiplyCommand { get; private set; }

        public ICommand Number7Command { get; private set; }

        public ICommand Number8Command { get; private set; }

        public ICommand Number9Command { get; private set; }

        public ICommand SubtractCommand { get; private set; }

        public ICommand Number4Command { get; private set; }

        public ICommand Number5Command { get; private set; }

        public ICommand Number6Command { get; private set; }

        public ICommand AddCommand { get; private set; }

        public ICommand Number1Command { get; private set; }

        public ICommand Number2Command { get; private set; }

        public ICommand Number3Command { get; private set; }

        public ICommand Number0Command { get; private set; }

        public ICommand DotCommand { get; private set; }

        public ICommand EquelCommand { get; private set; }

        #endregion

        #region Commands implementation

        public void Accept()
        {
            navigationData.Add("CalculatorResult", Result);

            NavigationService.Navigate(typeof(CreateTransactionPage), navigationData);
        }

        public void Cancel()
        {
            NavigationService.Navigate(typeof(CreateTransactionPage));
        }

        public void SetToZero()
        {
            Result = 0M;
            Expression = Expression.Clear();
        }

        public void Delete()
        {
            if (Expression.Length > 0)
                Expression = Expression.Remove(Expression.Length - 1, 1);
        }

        public void Divide()
        {
            char lastChar = (Expression.Length > 0) ? Expression[Expression.Length - 1] : ' ';

            if (char.IsDigit(lastChar))
                Expression = Expression.Append(" ÷ ");
        }

        public void Multiply()
        {
            char lastChar = (Expression.Length > 0) ? Expression[Expression.Length - 1] : ' ';

            if (char.IsDigit(lastChar))
                Expression = Expression.Append(" × ");
        }

        public void Subtract()
        {
            char lastChar = (Expression.Length > 0) ? Expression[Expression.Length - 1] : ' ';

            if (char.IsDigit(lastChar))
                Expression = Expression.Append(" - ");
        }

        public void Add()
        {
            char lastChar = (Expression.Length > 0) ? Expression[Expression.Length - 1] : ' ';

            if (char.IsDigit(lastChar))
                Expression = Expression.Append(" + ");
        }

        public void Dot()
        {
            char lastChar = (Expression.Length > 0) ? Expression[Expression.Length - 1] : ' ';

            if (char.IsDigit(lastChar))
                Expression = Expression.Append(".");
        }

        public void Equel()
        {
            char lastChar = (Expression.Length > 0) ? Expression[Expression.Length - 1] : ' ';

            bool hasExpression = (Expression.Length > 0 && Expression.ToString().LastIndexOf(' ') > 0) ? true : false;

            if (char.IsDigit(lastChar) && hasExpression)
            {
                Calculate();
            }
        }

        public void Number0()
        {
            char lastChar = (Expression.Length > 0) ? Expression[Expression.Length - 1] : ' ';

            if (char.IsDigit(lastChar) || lastChar.Equals('.'))
                Expression = Expression.Append(0M);
        }

        public void Number1()
        {
            Expression = Expression.Append(1M);
        }

        public void Number2()
        {
            Expression = Expression.Append(2M);
        }

        public void Number3()
        {
            Expression = Expression.Append(3M);
        }

        public void Number4()
        {
            Expression = Expression.Append(4M);
        }

        public void Number5()
        {
            Expression = Expression.Append(5M);
        }

        public void Number6()
        {
            Expression = Expression.Append(6M);
        }

        public void Number7()
        {
            Expression = Expression.Append(7M);
        }

        public void Number8()
        {
            Expression = Expression.Append(8M);
        }

        public void Number9()
        {
            Expression = Expression.Append(9M);
        }

        #endregion

        #region Properties

        private decimal _result;

        public decimal Result
        {
            get
            {
                return _result;
            }
            set
            {
                _result = value;
                OnPropertyChanged();
            }
        }

        private StringBuilder _expression;

        public StringBuilder Expression
        {
            get
            {
                if (_expression == null)
                    _expression = new StringBuilder();
                return _expression;
            }
            set
            {
                _expression = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Helping methods

        private void Calculate()
        {
            CalculationEngine engine = new CalculationEngine();

            Result = (decimal)engine.Calculate(Expression.ToString().Replace('÷', '/').Replace('×', '*'));
        }

        #endregion
    }
}
