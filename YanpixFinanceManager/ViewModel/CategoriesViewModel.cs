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
using YanpixFinanceManager.Model.Entities.Common;
using YanpixFinanceManager.Model.Entities.Enums;
using YanpixFinanceManager.View;
using YanpixFinanceManager.ViewModel.Common;

namespace YanpixFinanceManager.ViewModel
{
    public class CategoriesViewModel : BindableBase
    {
        #region Fields & Constructor

        private INavigationService _navigationService;

        private IEntityBaseService<Category> _categoryService;

        private IPlatformEvents _platformEvents;

        private Dictionary<string, object> _navigationData;

        public CategoriesViewModel(INavigationService navigationService,
            IEntityBaseService<Category> categoryService,
            IPlatformEvents platformEvents)
        {
            _navigationService = navigationService;
            _categoryService = categoryService;
            _platformEvents = platformEvents;
            _navigationData = new Dictionary<string, object>();

            _platformEvents.BackButtonPressed += BackButtonPressed;

            _selectedPivot = InitSelectedPivot();
        }

        #endregion

        #region Commands

        private ICommand _addNewCommand;

        public ICommand AddNewCommand
        {
            get
            {
                if (_addNewCommand == null)
                    _addNewCommand = new DelegateCommand((e) => AddNewAction(e));
                return _addNewCommand;
            }
            private set
            {
                _addNewCommand = value;
            }
        }

        private ICommand _clearSelectionCommand;

        public ICommand ClearSelectionCommand
        {
            get
            {
                if (_clearSelectionCommand == null)
                    _clearSelectionCommand = new DelegateCommand((e) => ClearSelectionAction(e));
                return _clearSelectionCommand;
            }
            private set
            {
                _clearSelectionCommand = value;
            }
        }

        private ICommand _deleteSelectedCommand;

        public ICommand DeleteSelectedCommand
        {
            get
            {
                if (_deleteSelectedCommand == null)
                    _deleteSelectedCommand = new DelegateCommand((e) => DeleteSelectedAction(e));
                return _deleteSelectedCommand;
            }
            private set
            {
                _deleteSelectedCommand = value;
            }
        }

        private ICommand _deleteAllCommand;

        public ICommand DeleteAllCommand
        {
            get
            {
                if (_deleteAllCommand == null)
                    _deleteAllCommand = new DelegateCommand((e) => DeleteAllAction(e));
                return _deleteAllCommand;
            }
            private set
            {
                _deleteAllCommand = value;
            }
        }

        #endregion

        #region Command Actions

        private void AddNewAction(object e)
        {
            _navigationData.Add("CategoriesSelectedPivot", SelectedPivot);
            _navigationService.Navigate(typeof(AddNewCategoryPage), _navigationData);
        }

        private void ClearSelectionAction(object e)
        {
            ClearSelection();
        }

        private void DeleteSelectedAction(object e)
        {
            if (SelectedPivot == 0)
            {
                foreach (Category category in IncomeCategories.Where(x => x.IsSelected == true).Select(x => x.Entity))
                {                    
                    _categoryService.Delete(category.Id);
                }
            }
            else if (SelectedPivot == 1)
            {
                foreach (Category category in ExpenceCategories.Where(x => x.IsSelected == true).Select(x => x.Entity))
                {           
                    _categoryService.Delete(category.Id);
                }
            }

            ClearSelection();

            UpdateCategories();
        }

        private void DeleteAllAction(object e)
        {
            if (SelectedPivot == 0)
            {
                _categoryService.DeleteAllIncomeCategories();
            }
            else if (SelectedPivot == 1)
            {
                _categoryService.DeleteAllExpenceCategories();
            }

            ClearSelection();

            UpdateCategories();
        }

        #endregion

        #region Properties

        private ObservableCollection<EntitySelection<Category>> _incomeCategories;

        public ObservableCollection<EntitySelection<Category>> IncomeCategories
        {
            get
            {
                if (_incomeCategories == null)
                    UpdateCategories();

                return _incomeCategories;
            }
            set
            {
                _incomeCategories = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<EntitySelection<Category>> _expenceCategories;

        public ObservableCollection<EntitySelection<Category>> ExpenceCategories
        {
            get
            {
                if (_expenceCategories == null)
                    UpdateCategories();

                return _expenceCategories;
            }
            set
            {
                _expenceCategories = value;
                OnPropertyChanged();
            }
        }

        private int _selectedPivot;

        public int SelectedPivot
        {
            get
            {
                return _selectedPivot;
            }
            set
            {
                _selectedPivot = value;
                OnPropertyChanged();
                ClearSelection();
            }
        }

        #endregion

        #region Platform Events

        private void BackButtonPressed(object sender, EventArgs e)
        {
            _navigationService.Navigate(typeof(MainPage));

            _platformEvents.BackButtonPressed -= BackButtonPressed;
        }

        #endregion

        #region Helping Methods

        private void ClearSelection()
        {
            if (SelectedPivot == 0)
            {
                foreach (var category in IncomeCategories)
                {
                    category.IsSelected = false;
                }
            }
            else if (SelectedPivot == 1)
            {
                foreach (var category in ExpenceCategories)
                {
                    category.IsSelected = false;
                }
            }
        }

        private void UpdateCategories()
        {
            if (SelectedPivot == 0)
                IncomeCategories = new ObservableCollection<EntitySelection<Category>>(
                    _categoryService.GetIncomeCategories()
                    .Select(x => new EntitySelection<Category>()
                    {
                        Entity = x
                    }));
            else if (SelectedPivot == 1)
                ExpenceCategories = new ObservableCollection<EntitySelection<Category>>(
                    _categoryService.GetExpenceCategories()
                    .Select(x => new EntitySelection<Category>()
                    {
                        Entity = x
                    }));
        }

        private int InitSelectedPivot()
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
