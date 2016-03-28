using FinanceManager.Common;
using FinanceManager.Infrastructure;
using FinanceManager.Model.DataAccess.Providers;
using FinanceManager.Model.Entities;
using FinanceManager.View;
using FinanceManager.ViewModel.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FinanceManager.ViewModel
{
    public class CreateCurrencyViewModel : BindableBase
    {
        public CreateCurrencyViewModel()
        {
            SaveCurrencyCommand = new RelayCommand(SaveCurrency);
            CancelCurrencyCommand = new RelayCommand(CancelCurrency);

            ValidationErrors = new ValidationErrors();
        }

        #region Services

        public INavigationService NavigationService { get; set; }

        public IDataServicesProvider DataService { get; set; }

        #endregion

        #region Commands

        public ICommand SaveCurrencyCommand { get; private set; }

        public ICommand CancelCurrencyCommand { get; private set; }

        #endregion

        #region Commands implementation

        public void SaveCurrency()
        {
            Validate();

            if (IsValid)
            {
                DataService.Get<Currency>().Save(Currency);

                NavigationService.Navigate(typeof(MainPage));
            }
            else
                return;
        }

        public void CancelCurrency()
        {
            NavigationService.Navigate(typeof(MainPage));
        }

        #endregion

        #region Properties

        public ValidationErrors ValidationErrors { get; set; }

        public bool IsValid { get; private set; }

        private Currency _currency;

        public Currency Currency
        {
            get
            {
                if (_currency == null)
                    _currency = new Currency();
                return _currency;
            }
            set
            {
                _currency = value;
                OnPropertyChanged();
            }
        }

        private void Validate()
        {
            ValidationErrors.Clear();

            Currency currency = DataService.Get<Currency>().GetAll().Where(x => x.Title.Equals(Currency.Title)).SingleOrDefault();

            if (string.IsNullOrEmpty(Currency.Title))
            {
                ValidationErrors["Title"] = "title is required";
            }
            else if (Currency.Title.Length > 20)
            {
                ValidationErrors["Title"] = "title's length should not exceed 20 characters";
            }
            else if (currency != null)
            {
                ValidationErrors["Title"] = "specified title already exists";
            }

            if (string.IsNullOrEmpty(Currency.Symbol))
            {
                ValidationErrors["Symbol"] = "symbol is required";
            }
            else if (Currency.Title.Length > 2)
            {
                ValidationErrors["Symbol"] = "symbol's length should not exceed 2 characters";
            }

            IsValid = ValidationErrors.IsValid;

            OnPropertyChanged("IsValid");
            OnPropertyChanged("ValidationErrors");
        }

        #endregion
    }
}
