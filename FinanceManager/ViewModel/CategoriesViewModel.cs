using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Input;
using Windows.UI.Popups;
using Windows.UI.Xaml;
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

        private string _categoryName;

        public string CategoryName
        {
            get { return _categoryName; }
            set
            {
                if (_categoryName != value)
                {
                    _categoryName = value;
                    OnPropertyChanged("CategoryName");
                }
            }
        }

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
        public static readonly DependencyProperty TestCommandProperty = DependencyProperty.Register("TestCommand", typeof (RelayCommand<object>), typeof (CategoriesViewModel), new PropertyMetadata(default(RelayCommand<object>)));

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



        #endregion

        #region Methods & Commands
        public RelayCommand OpenEditPopupCommand => new RelayCommand(() => IsOpen = (!IsOpen));
        public RelayCommand ClosePopup => new RelayCommand(() => IsOpen = false);

        public RelayCommand AddIncomeCategory => new RelayCommand(CreateNewIncomeCategory);
        public RelayCommand AddExpenseCategory => new RelayCommand(CreateNewExpenseCategory);

        public RelayCommand<short> DeleteExpenseCategoryCommand => new RelayCommand<short>(DeleteExpenseCategory);

        public RelayCommand<short> DeleteIncomeCategoryCommand => new RelayCommand<short>(DeleteIncomeCategory);
        private bool _isDeleteMode;
        public bool IsDeleteMode
        {
            get
            {
                return _isDeleteMode;
                
            }
            set
            {
                if (_isDeleteMode != value)
                {
                    _isDeleteMode = value;
                    OnPropertyChanged("IsDeleteMode");
                }
            }
        }

        public RelayCommand DeleteModeCommand => new RelayCommand(() => IsDeleteMode = (!IsDeleteMode));

        private void CreateNewExpenseCategory()
        {
            _categoriesService.CreateExpenseCategory(new ExpenseCategory
            {
                Name = CategoryName
            });

            ExpenseCategories = _categoriesService.GetAllExpenseCategories();
            CategoryName = string.Empty;
        }

        private void CreateNewIncomeCategory()
        {
            _categoriesService.CreateIncomeCategory(new IncomeCategory
            {
                Name = CategoryName
            });
            IncomeCategories = _categoriesService.GetAllIncomeCategories();
            CategoryName = string.Empty;
        }

        private async void DeleteExpenseCategory(short categoryId)
        {
            await _categoriesService.DeleteExpenseCategoryAsync(categoryId);

            ExpenseCategories = _categoriesService.GetAllExpenseCategories();
        }

        private async void DeleteIncomeCategory(short categoryId)
        {
            await _categoriesService.DeleteIncomeeCategoryAsync(categoryId);

            IncomeCategories = _categoriesService.GetAllIncomeCategories();
        }

        #endregion

    }
}
