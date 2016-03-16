using FinanceManager.Common;
using FinanceManager.Infrastructure;
using FinanceManager.Model.DataAccess.Services;
using FinanceManager.Model.Entities;
using FinanceManager.Model.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FinanceManager.ViewModel
{
    public class CategoriesViewModel : BaseViewModel
    {
        public CategoriesViewModel()
        {
            _incomeCategories = new List<Category>();
            _expenceCategories = new List<Category>();

            CreateCategoryCommand = new RelayCommand(CreateCategory);
            DeleteAllCategoriesCommand = new RelayCommand(DeleteAllCategories);
        }

        #region Services

        // Data service for money boxes
        public IDataService<Category> CategoriesDataService { get; set; }

        // Service for navigation
        public INavigationService NavigationService { get; set; }

        #endregion

        #region Commands

        public ICommand CreateCategoryCommand { get; private set; }

        public ICommand DeleteAllCategoriesCommand { get; private set; }

        #endregion

        #region Commands implementation

        public void CreateCategory()
        {
            //NavigationService.Navigate(typeof());
        }

        private void DeleteAllCategories()
        {
            CategoriesDataService.DeleteAll();
            LoadCategories();
        }

        #endregion

        #region Properties

        private List<Category> _incomeCategories;

        public List<Category> IncomeCategories
        {
            get
            {
                LoadCategories();
                return _incomeCategories;
            }
            set
            {
                _incomeCategories = value;
                OnPropertyChanged();
            }
        }

        private List<Category> _expenceCategories;

        public List<Category> ExpenceCategories
        {
            get
            {
                LoadCategories();
                return _expenceCategories;
            }
            set
            {
                _expenceCategories = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Helping methods

        private void LoadCategories()
        {
            IncomeCategories = CategoriesDataService.GetAll()
                .Where(c => c.Type == TransactionType.Income)
                .OrderBy(c => c.Title)
                .ToList();

            ExpenceCategories = CategoriesDataService.GetAll()
                .Where(c => c.Type == TransactionType.Expence)
                .OrderBy(c => c.Title)
                .ToList();
        }

        #endregion
    }
}
