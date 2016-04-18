using Jace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using YanpixFinanceManager.View;
using YanpixFinanceManager.ViewModel.Common;

namespace YanpixFinanceManager.ViewModel
{
    public class CreateTransactionViewModel : BindableBase
    {
        #region Fields & Constructor

        private INavigationService _navigationService;

        private IPlatformEvents _platformEvents;

        public CreateTransactionViewModel(INavigationService navigationService, 
            IPlatformEvents platformEvents)
        {
            _navigationService = navigationService;
            _platformEvents = platformEvents;

            _platformEvents.BackButtonPressed += BackButtonPressed;
        }

        #endregion

        #region Commands

        private ICommand _acceptCommand;

        public ICommand AcceptCommand
        {
            get
            {
                if (_acceptCommand == null)
                    _acceptCommand = new DelegateCommand((e) => AcceptAction(e));
                return _acceptCommand;
            }
            private set
            {
                _acceptCommand = value;
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

        #region Calculator Commands

        private ICommand _number0Command;

        public ICommand Number0Command
        {
            get
            {
                if (_number0Command == null)
                    _number0Command = new DelegateCommand((e) => Number0Action(e));
                return _number0Command;
            }
            private set
            {
                _number0Command = value;
            }
        }

        private ICommand _number1Command;

        public ICommand Number1Command
        {
            get
            {
                if (_number1Command == null)
                    _number1Command = new DelegateCommand((e) => Number1Action(e));
                return _number1Command;
            }
            private set
            {
                _number1Command = value;
            }
        }

        private ICommand _number2Command;

        public ICommand Number2Command
        {
            get
            {
                if (_number2Command == null)
                    _number2Command = new DelegateCommand((e) => Number2Action(e));
                return _number2Command;
            }
            private set
            {
                _number2Command = value;
            }
        }

        private ICommand _number3Command;

        public ICommand Number3Command
        {
            get
            {
                if (_number3Command == null)
                    _number3Command = new DelegateCommand((e) => Number3Action(e));
                return _number3Command;
            }
            private set
            {
                _number3Command = value;
            }
        }

        private ICommand _number4Command;

        public ICommand Number4Command
        {
            get
            {
                if (_number4Command == null)
                    _number4Command = new DelegateCommand((e) => Number4Action(e));
                return _number4Command;
            }
            private set
            {
                _number4Command = value;
            }
        }

        private ICommand _number5Command;

        public ICommand Number5Command
        {
            get
            {
                if (_number5Command == null)
                    _number5Command = new DelegateCommand((e) => Number5Action(e));
                return _number5Command;
            }
            private set
            {
                _number5Command = value;
            }
        }

        private ICommand _number6Command;

        public ICommand Number6Command
        {
            get
            {
                if (_number6Command == null)
                    _number6Command = new DelegateCommand((e) => Number6Action(e));
                return _number6Command;
            }
            private set
            {
                _number6Command = value;
            }
        }

        private ICommand _number7Command;

        public ICommand Number7Command
        {
            get
            {
                if (_number7Command == null)
                    _number7Command = new DelegateCommand((e) => Number7Action(e));
                return _number7Command;
            }
            private set
            {
                _number7Command = value;
            }
        }

        private ICommand _number8Command;

        public ICommand Number8Command
        {
            get
            {
                if (_number8Command == null)
                    _number8Command = new DelegateCommand((e) => Number8Action(e));
                return _number8Command;
            }
            private set
            {
                _number8Command = value;
            }
        }

        private ICommand _number9Command;

        public ICommand Number9Command
        {
            get
            {
                if (_number9Command == null)
                    _number9Command = new DelegateCommand((e) => Number9Action(e));
                return _number9Command;
            }
            private set
            {
                _number9Command = value;
            }
        }

        private ICommand _setToZeroCommand;

        public ICommand SetToZeroCommand
        {
            get
            {
                if (_setToZeroCommand == null)
                    _setToZeroCommand = new DelegateCommand((e) => SetToZeroAction(e));
                return _setToZeroCommand;
            }
            private set
            {
                _setToZeroCommand = value;
            }
        }

        private ICommand _deleteCommand;

        public ICommand DeleteCommand
        {
            get
            {
                if (_deleteCommand == null)
                    _deleteCommand = new DelegateCommand((e) => DeleteAction(e));
                return _deleteCommand;
            }
            private set
            {
                _deleteCommand = value;
            }
        }

        private ICommand _divideCommand;

        public ICommand DivideCommand
        {
            get
            {
                if (_divideCommand == null)
                    _divideCommand = new DelegateCommand((e) => DivideAction(e));
                return _divideCommand;
            }
            private set
            {
                _divideCommand = value;
            }
        }

        private ICommand _multiplyCommand;

        public ICommand MultiplyCommand
        {
            get
            {
                if (_multiplyCommand == null)
                    _multiplyCommand = new DelegateCommand((e) => MultiplyAction(e));
                return _multiplyCommand;
            }
            private set
            {
                _multiplyCommand = value;
            }
        }

        private ICommand _subtractCommand;

        public ICommand SubtractCommand
        {
            get
            {
                if (_subtractCommand == null)
                    _subtractCommand = new DelegateCommand((e) => SubtractAction(e));
                return _subtractCommand;
            }
            private set
            {
                _subtractCommand = value;
            }
        }

        private ICommand _addCommand;

        public ICommand AddCommand
        {
            get
            {
                if (_addCommand == null)
                    _addCommand = new DelegateCommand((e) => AddAction(e));
                return _addCommand;
            }
            private set
            {
                _addCommand = value;
            }
        }

        private ICommand _dotCommand;

        public ICommand DotCommand
        {
            get
            {
                if (_dotCommand == null)
                    _dotCommand = new DelegateCommand((e) => DotAction(e));
                return _dotCommand;
            }
            private set
            {
                _dotCommand = value;
            }
        }

        private ICommand _equalCommand;

        public ICommand EqualCommand
        {
            get
            {
                if (_equalCommand == null)
                    _equalCommand = new DelegateCommand((e) => EqualAction(e));
                return _equalCommand;
            }
            private set
            {
                _equalCommand = value;
            }
        }

        #endregion

        #endregion

        #region Command Actions

        private void AcceptAction(object e)
        {
            ;
        }

        private void CancelAction(object e)
        {
            ;
        }

        #region Calculator Command Actions

        private void Number0Action(object e)
        {
            char lastChar = (Expression.Length > 0) ? Expression[Expression.Length - 1] : ' ';

            if (char.IsDigit(lastChar) || lastChar.Equals('.'))
                Expression = Expression + 0;
        }

        private void Number1Action(object e)
        {
            Expression = Expression + 1;
        }

        private void Number2Action(object e)
        {
            Expression = Expression + 2;
        }

        private void Number3Action(object e)
        {
            Expression = Expression + 3;
        }

        private void Number4Action(object e)
        {
            Expression = Expression + 4;
        }

        private void Number5Action(object e)
        {
            Expression = Expression + 5;
        }

        private void Number6Action(object e)
        {
            Expression = Expression + 6;
        }

        private void Number7Action(object e)
        {
            Expression = Expression + 7;
        }

        private void Number8Action(object e)
        {
            Expression = Expression + 8;
        }

        private void Number9Action(object e)
        {
            Expression = Expression + 9;
        }

        private void SetToZeroAction(object e)
        {
            Result = 0M;
            Expression = "";
        }

        private void DeleteAction(object e)
        {
            if (Expression.Length > 0)
                Expression = Expression.Remove(Expression.Length - 1, 1);
        }

        private void DivideAction(object e)
        {
            char lastChar = (Expression.Length > 0) ? Expression[Expression.Length - 1] : ' ';

            if (char.IsDigit(lastChar))
                Expression = Expression + "÷";
        }

        private void MultiplyAction(object e)
        {
            char lastChar = (Expression.Length > 0) ? Expression[Expression.Length - 1] : ' ';

            if (char.IsDigit(lastChar))
                Expression = Expression + "×";
        }

        private void SubtractAction(object e)
        {
            char lastChar = (Expression.Length > 0) ? Expression[Expression.Length - 1] : ' ';

            if (char.IsDigit(lastChar))
                Expression = Expression + "-";
        }

        private void AddAction(object e)
        {
            char lastChar = (Expression.Length > 0) ? Expression[Expression.Length - 1] : ' ';

            if (char.IsDigit(lastChar))
                Expression = Expression + "+";
        }

        private void DotAction(object e)
        {
            char lastChar = (Expression.Length > 0) ? Expression[Expression.Length - 1] : ' ';

            if (char.IsDigit(lastChar))
                Expression = Expression + ".";
        }

        private void EqualAction(object e)
        {
            char lastChar = (Expression.Length > 0) ? Expression[Expression.Length - 1] : ' ';

            if (char.IsDigit(lastChar) && Expression.Length > 0)
            {
                Calculate();
            }
        }

        #endregion

        #endregion

        #region Properties

        #region Calculator Properties

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

        private string _expression = "";

        public string Expression
        {
            get
            {
                return _expression;
            }
            set
            {
                _expression = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #endregion

        #region Helping methods

        private void Calculate()
        {
            CalculationEngine engine = new CalculationEngine();

            Result = (decimal)engine.Calculate(Expression.Replace('÷', '/').Replace('×', '*'));
        }

        #endregion

        #region Platform Events

        private void BackButtonPressed(object sender, EventArgs e)
        {
            _navigationService.Navigate(typeof(MoneyBoxPage));

            _platformEvents.BackButtonPressed -= BackButtonPressed;
        }

        #endregion
    }
}
