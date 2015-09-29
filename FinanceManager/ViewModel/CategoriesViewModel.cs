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
            UpdataData();
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

        private ObservableCollection<Category> _categories;

        public ObservableCollection<Category> Categories
        {
            get { return _categories; }
            set
            {
                if (_categories != value)
                {
                    _categories = value;
                    OnPropertyChanged("Categories");
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
        public RelayCommand<short> DeleteCategoryCommand => new RelayCommand<short>(DeleteCategory);
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
            _categoriesService.CreateCategory(new Category
            {
                Name = CategoryName,
                CategoryType = CategoryTypeEnum.Expense
            });

            UpdataData();
            CategoryName = string.Empty;
        }

        private void CreateNewIncomeCategory()
        {
            _categoriesService.CreateCategory(new Category
            {
                Name = CategoryName,
                CategoryType = CategoryTypeEnum.Income
                
            });
            UpdataData();
            CategoryName = string.Empty;
        }

        private ObservableCollection<Category> _incomeCategories;
        public ObservableCollection<Category> IncomeCategories
        {
            get
            {
               return _incomeCategories;
            }
            set
            {
                if (_incomeCategories != value)
                {
                    _incomeCategories = value;
                    OnPropertyChanged("IncomeCategories");
                }
            }
        }

        private ObservableCollection<Category> _expenseCategories;

        public ObservableCollection<Category> ExpenseCategories
        {
            get
            {
                return _expenseCategories;
            }
            set
            {
                if (_expenseCategories != value)
                {
                    _expenseCategories = value;
                    OnPropertyChanged("ExpenseCategories");
                }
            }
        }

        private async void DeleteCategory(short categoryId)
        {
            await _categoriesService.DeleteCategoryAsync(categoryId);

            UpdataData();
        }

        private void UpdataData()
        {
            Categories = _categoriesService.GetAllCategories();
            IncomeCategories = new ObservableCollection<Category>(Categories.Select(x => x).Where(x => x.CategoryType == CategoryTypeEnum.Income));
            ExpenseCategories = new ObservableCollection<Category>(Categories.Select(x => x).Where(x => x.CategoryType == CategoryTypeEnum.Expense));
        }
        #endregion

    }
}
