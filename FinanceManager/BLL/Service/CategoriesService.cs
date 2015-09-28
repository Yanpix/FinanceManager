using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinanceManager.BLL.IService;
using FinanceManager.DAL.Models;
using FinanceManager.DAL.UnitOfWork;

namespace FinanceManager.BLL.Service
{
    public class CategoriesService : ICategoriesService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoriesService()
        {
            _unitOfWork = new UnitOfWork();
        }
        public async Task<ObservableCollection<ExpenseCategory>> GetAllExpenseCategoriesAsync()
        {
            return await _unitOfWork.Repository<ExpenseCategory>().GetAsync();
        }

        public async Task<ObservableCollection<IncomeCategory>> GetAllIncomeCategoriesAsync()
        {
            return await _unitOfWork.Repository<IncomeCategory>().GetAsync();
        }

        public async Task CreateExpenseCategoryAsync(ExpenseCategory category)
        {
            await _unitOfWork.Repository<ExpenseCategory>().CreateAsync(category);
        }

        public async Task CreateIncomeCategoryAsync(IncomeCategory category)
        {
            await _unitOfWork.Repository<IncomeCategory>().CreateAsync(category);
        }

        public async Task DeleteExpenseCategoryAsync(int id)
        {
          await _unitOfWork.Repository<ExpenseCategory>().DeleteAsync(id);
        }

        public async Task DeleteIncomeeCategoryAsync(int id)
        {
            await _unitOfWork.Repository<IncomeCategory>().DeleteAsync(id);
        }

        public async Task DeleteAllExpenseCategory()
        {
            await _unitOfWork.Repository<ExpenseCategory>().DeleteAll();
        }

        public async Task DeleteAllIncomeCategory()
        {
            await _unitOfWork.Repository<IncomeCategory>().DeleteAll();
        }

        public ObservableCollection<ExpenseCategory> GetAllExpenseCategories()
        {
            return _unitOfWork.Repository<ExpenseCategory>().Get();
        }

        public ObservableCollection<IncomeCategory> GetAllIncomeCategories()
        {
            return _unitOfWork.Repository<IncomeCategory>().Get();
        }

        public void CreateExpenseCategory(ExpenseCategory category)
        {
            _unitOfWork.Repository<ExpenseCategory>().Create(category);
        }

        public void CreateIncomeCategory(IncomeCategory category)
        {
            _unitOfWork.Repository<IncomeCategory>().Create(category);
        }
    }
}
