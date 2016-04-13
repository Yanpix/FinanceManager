using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using YanpixFinanceManager.Model.DataAccess.Extensions;
using YanpixFinanceManager.Model.DataAccess.Services;
using YanpixFinanceManager.Model.Entities;
using YanpixFinanceManager.Model.Entities.Enums;
using YanpixFinanceManager.View;
using YanpixFinanceManager.ViewModel.Common;

namespace YanpixFinanceManager.ViewModel
{
    public class AddNewCategoryViewModel : BindableBase
    {
        #region Fields & Constructor

        private INavigationService _navigationService;

        private IEntityBaseService<Category> _categoryService;

        private IPlatformEvents _platformEvents;

        private Dictionary<string, object> _navigationData;

        public AddNewCategoryViewModel(INavigationService navigationService,
            IEntityBaseService<Category> categoryService,
            IPlatformEvents platformEvents)
        {
            _navigationService = navigationService;
            _categoryService = categoryService;
            _platformEvents = platformEvents;
            _navigationData = new Dictionary<string, object>();

            _platformEvents.BackButtonPressed += BackButtonPressed;

            _categoryType = InitCategoryType();
        }

        #endregion

        #region Commands

        private ICommand _saveCommand;

        public ICommand SaveCommand
        {
            get
            {
                if (_saveCommand == null)
                    _saveCommand = new DelegateCommand((e) => SaveAction(e));
                return _saveCommand;
            }
            private set
            {
                _saveCommand = value;
            }
        }

        #endregion

        #region Command Actions

        private void SaveAction(object e)
        {
            Validate();

            if (IsValid)
            {
                Category.Type = (TransactionType)Enum.Parse(typeof(TransactionType), CategoryType.ToString());

                _categoryService.Insert(Category, false);

                _navigationData.Add("CategoriesSelectedPivot", CategoryType);

                _navigationService.Navigate(typeof(CategoriesPage), _navigationData);
            }
        }

        #endregion

        #region Properties

        private ObservableCollection<string> _images;

        public ObservableCollection<string> Images
        {
            get
            {
                if (_images == null)
                    _images = new ObservableCollection<string>()
                    {
                        "/Assets/Icons/Light/appbar.cabinet.files.light.png",
                        "/Assets/Icons/Light/appbar.currency.dollar.light.png",
                        "/Assets/Icons/Light/appbar.drinks.beer.png",
                        "/Assets/Icons/Light/appbar.food.silverware.png",
                        "/Assets/Icons/Light/appbar.gift.png",
                        "/Assets/Icons/Light/appbar.journal.light.png",
                        "/Assets/Icons/Light/appbar.lightbulb.png",
                        "/Assets/Icons/Light/appbar.list.hidden.light.png",
                        "/Assets/Icons/Light/appbar.money.light.png",
                        "/Assets/Icons/Light/appbar.settings.light.png"
                    };

                return _images;
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

        private int _categoryType;

        public int CategoryType
        {
            get
            {
                return _categoryType;
            }
            set
            {
                _categoryType = value;
                OnPropertyChanged("IncomeType");
                OnPropertyChanged("ExpenceType");
            }
        }

        public bool IncomeType
        {
            get { return CategoryType.Equals(0); }
            set { CategoryType = 0; }
        }

        public bool ExpenceType
        {
            get { return CategoryType.Equals(1); }
            set { CategoryType = 1; }
        }

        #endregion

        #region Validation

        private ValidationErrors _validationErrors;

        public ValidationErrors ValidationErrors
        {
            get
            {
                if (_validationErrors == null)
                    _validationErrors = new ValidationErrors();
                return _validationErrors;
            }
            set
            {
                _validationErrors = value;
                OnPropertyChanged();
            }
        }

        private bool _isValid = false;

        public bool IsValid
        {
            get
            {
                return _isValid;
            }
            private set
            {
                _isValid = value;
                OnPropertyChanged();
            }
        }

        private void Validate()
        {
            ValidationErrors.Clear();

            Category existingCategory = _categoryService.ExistingCategory(Category.Title, CategoryType);

            if (string.IsNullOrEmpty(Category.Title))
                ValidationErrors["Title"] = "Title is required";
            else if (Category.Title.Length > 20)
                ValidationErrors["Title"] = "Title must contain up to 20 characters";
            else if (existingCategory != null)
                ValidationErrors["Title"] = "Category already exists";

            if (string.IsNullOrEmpty(Category.Image))
                ValidationErrors["Image"] = "Image is required";

            IsValid = ValidationErrors.IsValid;

            OnPropertyChanged("ValidationErrors");
        }

        #endregion

        #region Platform Events

        private void BackButtonPressed(object sender, EventArgs e)
        {
            _navigationService.Navigate(typeof(CategoriesPage));

            _platformEvents.BackButtonPressed -= BackButtonPressed;
        }

        #endregion

        #region Helping Methods

        private int InitCategoryType()
        {
            object selectedPivotItem = _navigationService.GetNavigationData("CategoriesSelectedPivot");

            if (selectedPivotItem != null)
                return (int)selectedPivotItem;
            else
                return 0;
        }

        #endregion
    }
}
