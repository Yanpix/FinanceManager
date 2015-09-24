using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Input;
using Windows.UI.Popups;
using FinanceManager.Base;
using FinanceManager.BLL.IService;
using FinanceManager.DAL.Models;
using FinanceManager.Pages;

namespace FinanceManager.ViewModel
{
    public class CategoriesViewModel : BindableBase
    {
        private readonly INavigationService _navigationService;
        private readonly ICategoriesService _categoriesService;

        public CategoriesViewModel(INavigationService navigationService, ICategoriesService categoriesService)
        {
            _categoriesService = categoriesService;
            _navigationService = navigationService;

            //EditIncomeCategories = _categoriesService.GetAllIncomeCategoriesAsync();
        }

        #region Fields and Properties

        public string CategoryName { get; set; }

        private bool _isOpen;

        public bool IsOpen
        {
            get { return _isOpen; }
            set
            {
                if (_isOpen != value)
                {
                    _isOpen = value;
                    OnPropertyChanged("IsOpen");
                }
            }
        }

        public Task<ObservableCollection<IncomeCategory>> EditIncomeCategories { get; set; }
        //private ObservableCollection<EditIncomeCategory> _editExpenseCategories;


        #endregion

        #region Navigation
        public RelayCommand EditIncomeCategoriesCommand => new RelayCommand(() => _navigationService.Navigate(typeof(EditIncomeCategory)));
        public RelayCommand EditExpenseCategoriesCommand => new RelayCommand(() => _navigationService.Navigate(typeof(EditExpenseCategory)));
        public RelayCommand OpenEditPopupCommand => new RelayCommand(() => IsOpen = (!IsOpen));
        public RelayCommand ClosePopup => new RelayCommand(() => IsOpen = false);

        public RelayCommand AddIncomeCategory => new RelayCommand(() => _categoriesService.CreateIncomeCategoryAsync(new IncomeCategory {Id = 1, Name = CategoryName}));
        public RelayCommand AddExpenseCategory => new RelayCommand(() => _categoriesService.CreateExpenseCategoryAsync(new ExpenseCategory { Id = 1, Name = CategoryName }) );

        #endregion

        #region Commands

        #endregion

        #region Methods




        #endregion


    }
}
