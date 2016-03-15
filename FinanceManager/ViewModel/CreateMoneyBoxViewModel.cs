﻿using FinanceManager.Common;
using FinanceManager.Infrastructure;
using FinanceManager.Model.DataAccess.Services;
using FinanceManager.Model.Entities;
using FinanceManager.View;
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
    public class CreateMoneyBoxViewModel : BaseViewModel
    {
        // View model constructor
        public CreateMoneyBoxViewModel()
        {
            // Initialize properties
            _moneyBox = new MoneyBox();
            _currencies = new List<Currency>();

            // Initialize commands
            SaveMoneyBoxCommand = new RelayCommand(SaveMoneyBox);
            CancelMoneyBoxCommand = new RelayCommand(CancelMoneyBox);
        }

        #region Services

        // Data service for money boxes
        public IDataService<MoneyBox> MoneyBoxesDataService { get; set; }

        // Data service for currencies
        public IDataService<Currency> CurrenciesDataService { get; set; }

        // Service for navigation
        public INavigationService NavigationService { get; set; }

        #endregion

        #region Commands

        public ICommand SaveMoneyBoxCommand { get; private set; }

        public ICommand CancelMoneyBoxCommand { get; private set; }

        #endregion

        #region Commands implementation

        public void SaveMoneyBox()
        {
            MoneyBox.CreationDate = DateTime.Now;
            MoneyBox.LastModifiedDate = DateTime.Now;
            MoneyBox.Balance = 0.0M;

            MoneyBoxesDataService.Create(MoneyBox);

            NavigationService.Navigate(typeof(MoneyBoxPage));
        }       

        public void CancelMoneyBox()
        {
            NavigationService.Navigate(typeof(MainPage));
        }

        #endregion

        #region Properties

        private MoneyBox _moneyBox;

        public MoneyBox MoneyBox
        {
            get
            {
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
                LoadCurrencies();
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

        private void LoadCurrencies()
        {
            Currencies = CurrenciesDataService.GetAll();
        }

        #endregion
    }
}
