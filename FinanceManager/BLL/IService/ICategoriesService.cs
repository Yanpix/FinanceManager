using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinanceManager.DAL.Models;

namespace FinanceManager.BLL.IService
{
    public interface ICategoriesService
    {
        Task<ObservableCollection<ExpenseCategory>> GetAllExpenseCategoriesAsync();
        Task<ObservableCollection<IncomeCategory>> GetAllIncomeCategoriesAsync();
        Task CreateExpenseCategoryAsync(ExpenseCategory category);
        Task CreateIncomeCategoryAsync(IncomeCategory category);
        Task DeleteExpenseCategoryAsync(int id);
        Task DeleteIncomeeCategoryAsync(int id);
        Task DeleteAllExpenseCategory();
        Task DeleteAllIncomeCategory();

        
        ObservableCollection<ExpenseCategory> GetAllExpenseCategories();
        ObservableCollection<IncomeCategory> GetAllIncomeCategories();
        void CreateExpenseCategory(ExpenseCategory category);
        void CreateIncomeCategory(IncomeCategory category);
        void DeleteExpenseCategory(int id);
        void DeleteIncomeCategory(int id);

    }

}
