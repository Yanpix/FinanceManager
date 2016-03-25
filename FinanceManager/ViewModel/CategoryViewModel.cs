using FinanceManager.Infrastructure;
using FinanceManager.Model.DataAccess.Providers;
using FinanceManager.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManager.ViewModel
{
    public class CategoryViewModel : BaseViewModel
    {
        public CategoryViewModel()
        {

        }

        #region Services

        public INavigationService NavigationService { get; set; }

        public IDataServicesProvider DataService { get; set; }

        #endregion

        #region Properties

        private Category _category;

        public Category Category
        {
            get
            {
                if (_category == null)
                    _category = LoadCategory();
                return _category;
            }
            set
            {
                _category = value;
                OnPropertyChanged();
            }
        }

        private List<Transaction> _transactions;

        public List<Transaction> Transactions
        {
            get
            {
                if (_transactions == null)
                    _transactions = LoadTransactions();
                return _transactions;
            }
            set
            {
                _transactions = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region HelpingMethods

        private Category LoadCategory()
        {
            object data = NavigationService.GetNavigationData("CategoryId");

            if (data != null)
                return DataService.Get<Category>().Get((int)data);
            else
                return null;
        }

        private List<Transaction> LoadTransactions()
        {
            return DataService.Get<Transaction>()
                .GetAll()
                .Where(t => t.CategoryId == Category.Id)
                .OrderByDescending(o => o.CreationDate)
                .ToList();
        }

        #endregion
    }
}
