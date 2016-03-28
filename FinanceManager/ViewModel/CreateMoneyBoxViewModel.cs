using FinanceManager.Common;
using FinanceManager.Infrastructure;
using FinanceManager.Model.DataAccess.Providers;
using FinanceManager.Model.DataAccess.Services;
using FinanceManager.Model.Entities;
using FinanceManager.View;
using FinanceManager.ViewModel.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace FinanceManager.ViewModel
{
    /// <summary>
    /// View model for CreateMoneyBoxPage, implements BaseViewModel
    /// </summary>
    public class CreateMoneyBoxViewModel : BindableBase
    {
        // View model constructor
        public CreateMoneyBoxViewModel()
        {
            navigationData = new Dictionary<string, object>();

            ValidationErrors = new ValidationErrors();

            // Initialize commands
            SaveMoneyBoxCommand = new RelayCommand(SaveMoneyBox);
            CancelMoneyBoxCommand = new RelayCommand(CancelMoneyBox);
        }

        Dictionary<string, object> navigationData;

        #region Services

        public INavigationService NavigationService { get; set; }

        public IDataServicesProvider DataService { get; set; }

        #endregion

        #region Commands

        public ICommand SaveMoneyBoxCommand { get; private set; }

        public ICommand CancelMoneyBoxCommand { get; private set; }

        #endregion

        #region Commands implementation

        public void SaveMoneyBox()
        {
            Validate();

            if (ValidationErrors.IsValid)
            {
                MoneyBox.CreationDate = DateTime.Now;
                MoneyBox.LastModifiedDate = DateTime.Now;
                MoneyBox.Balance = 0.0M;

                DataService.Get<MoneyBox>().Save(MoneyBox);

                navigationData.Add("MoneyBoxId", MoneyBox.Id);

                NavigationService.Navigate(typeof(MoneyBoxPage), navigationData);
            }
            else
                return;
        }       

        public void CancelMoneyBox()
        {
            NavigationService.Navigate(typeof(MainPage));
        }

        #endregion

        #region Properties

        public ValidationErrors ValidationErrors { get; set; }

        public bool IsValid { get; private set; }

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

        private List<Currency> _currencies;

        public List<Currency> Currencies
        {
            get
            {
                if (_currencies == null)
                    _currencies = LoadCurrencies();
                return _currencies;
            }
            set
            {
                _currencies = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Helping methods

        private List<Currency> LoadCurrencies()
        {
            return DataService.Get<Currency>().GetAll();
        }

        private void Validate()
        {
            ValidationErrors.Clear();

            MoneyBox moneyBox = DataService.Get<MoneyBox>().GetAll().Where(x => x.Title.Equals(MoneyBox.Title)).SingleOrDefault();

            if (string.IsNullOrEmpty(MoneyBox.Title))
            {
                ValidationErrors["Title"] = "title is required";
            }
            else if (MoneyBox.Title.Length > 50)
            {
                ValidationErrors["Title"] = "title's length should not exceed 50 characters";
            }
            else if(moneyBox != null)
            {
                ValidationErrors["Title"] = "specified title already exists";
            }

            if (MoneyBox.PrimaryCurrency == null)
            {
                ValidationErrors["Currency"] = "primary currency is required";
            }

            IsValid = ValidationErrors.IsValid;

            OnPropertyChanged("IsValid");
            OnPropertyChanged("ValidationErrors");
        }

        #endregion
    }
}
