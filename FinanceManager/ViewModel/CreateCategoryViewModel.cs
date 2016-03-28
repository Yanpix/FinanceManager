using FinanceManager.Common;
using FinanceManager.Infrastructure;
using FinanceManager.Model.DataAccess.Providers;
using FinanceManager.Model.Entities;
using FinanceManager.Model.Entities.Enums;
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
    public class CreateCategoryViewModel : BindableBase
    {
        public CreateCategoryViewModel()
        {
            SaveCategoryCommand = new RelayCommand(SaveCategory);
            CancelCategoryCommand = new RelayCommand(CancelCategory);

            ValidationErrors = new ValidationErrors();
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
            Validate();

            if (IsValid)
            {
                Category.Type = (TransactionType)Enum.Parse(typeof(TransactionType), SelectedType);
                DataService.Get<Category>().Save(Category);

                NavigationService.Navigate(typeof(MainPage));
            }
            else
                return;
        }

        public void CancelCategory()
        {
            NavigationService.Navigate(typeof(MainPage));
        }

        #endregion

        #region Properties

        public ValidationErrors ValidationErrors { get; set; }

        public bool IsValid { get; private set; }

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

        private void Validate()
        {
            ValidationErrors.Clear();

            Category category = DataService.Get<Category>().GetAll().Where(x => x.Title.Equals(Category.Title)).SingleOrDefault();

            if (string.IsNullOrEmpty(Category.Title))
            {
                ValidationErrors["Title"] = "title is required";
            }
            else if (Category.Title.Length > 20)
            {
                ValidationErrors["Title"] = "title's length should not exceed 20 characters";
            }
            else if (category != null)
            {
                ValidationErrors["Title"] = "specified title already exists";
            }

            if (string.IsNullOrEmpty(SelectedType))
            {
                ValidationErrors["Type"] = "primary currency is required";
            }

            IsValid = ValidationErrors.IsValid;

            OnPropertyChanged("IsValid");
            OnPropertyChanged("ValidationErrors");
        }

        #endregion
    }
}
