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

            IncomeCategories = _categoriesService.GetAllIncomeCategories();

            ExpenseCategories = _categoriesService.GetAllExpenseCategories();
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

        private ObservableCollection<IncomeCategory> _incomeCategories;

        public ObservableCollection<IncomeCategory> IncomeCategories
        {
            get { return _incomeCategories; }
            set
            {
                if (_incomeCategories != value)
                {
                    _incomeCategories = value;
                    OnPropertyChanged("IncomeCategories");
                }
            }
        }

        private ObservableCollection<ExpenseCategory> _expenseCategories;

        public ObservableCollection<ExpenseCategory> ExpenseCategories
        {
            get { return _expenseCategories; }
            set
            {
                if (_expenseCategories != value)
                {
                    _expenseCategories = value;
                    OnPropertyChanged("ExpenseCategories");
                }
            }
        }

        #endregion

        #region Navigation
        public RelayCommand EditIncomeCategoriesCommand => new RelayCommand(() => _navigationService.Navigate(typeof(EditIncomeCategory)));
        public RelayCommand EditExpenseCategoriesCommand => new RelayCommand(() => _navigationService.Navigate(typeof(EditExpenseCategory)));
        public RelayCommand OpenEditPopupCommand => new RelayCommand(() => IsOpen = (!IsOpen));
        public RelayCommand ClosePopup => new RelayCommand(() => IsOpen = false);

        public RelayCommand AddIncomeCategory => new RelayCommand(CreateNewIncomeCategory);
        public RelayCommand AddExpenseCategory => new RelayCommand(CreateNewExpenseCategory);
        private void CreateNewExpenseCategory()
        {
           _categoriesService.CreateExpenseCategory(new ExpenseCategory
            {
                Name = CategoryName
            });
            
            ExpenseCategories = _categoriesService.GetAllExpenseCategories();
        }

        private void CreateNewIncomeCategory()
        {
            _categoriesService.CreateIncomeCategory(new IncomeCategory
            {
                Name = CategoryName
            });
            IncomeCategories = _categoriesService.GetAllIncomeCategories();
        }


        #endregion

        #region Commands

        #endregion

        #region Methods




        #endregion


    }
}
