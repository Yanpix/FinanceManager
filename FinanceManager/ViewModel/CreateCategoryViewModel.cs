using FinanceManager.Common;
using FinanceManager.Infrastructure;
using FinanceManager.Model.DataAccess.Providers;
using FinanceManager.Model.Entities;
using FinanceManager.Model.Entities.Enums;
using FinanceManager.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FinanceManager.ViewModel
{
    public class CreateCategoryViewModel : BaseViewModel
    {
        public CreateCategoryViewModel()
        {
            SaveCategoryCommand = new RelayCommand(SaveCategory);
            CancelCategoryCommand = new RelayCommand(CancelCategory);
        }

        #region Services

        public INavigationService NavigationService { get; set; }

        public IDataServicesProvider DataService { get; set; }

        #endregion

        #region Commands

        public ICommand SaveCategoryCommand { get; private set; }

        public ICommand CancelCategoryCommand { get; private set; }

        #endregion

        #region Commands implementation

        public void SaveCategory()
        {
            Category.Type = (TransactionType)Enum.Parse(typeof(TransactionType), SelectedType);
            DataService.Get<Category>().Save(Category);

            NavigationService.Navigate(typeof(MainPage));
        }

        public void CancelCategory()
        {
            NavigationService.Navigate(typeof(MainPage));
        }

        #endregion

        #region Properties

        private List<string> _types;

        public List<string> Types
        {
            get
            {
                return Enum.GetNames(typeof(TransactionType)).ToList();
            }
            set
            {
                _types = value;
                OnPropertyChanged();
            }
        }

        private string _selectedType;

        public string SelectedType
        {
            get
            {
                return _selectedType;
            }
            set
            {
                _selectedType = value;
                OnPropertyChanged();
            }
        }

        private Category _category;

        public Category Category
        {
            get
            {
                if (_category == null)
                    _category = new Category();
                return _category;
            }
            set
            {
                _category = value;
                OnPropertyChanged();
            }
        }

        #endregion
    }
}
