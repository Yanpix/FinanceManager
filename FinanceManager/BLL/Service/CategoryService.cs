using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinanceManager.DAL.Models;
using FinanceManager.DAL.Repository;

namespace FinanceManager.BLL.Service
{
    public class CategoryService
    {
        private readonly IGenericRepository<ExpenseCategory> _genericRepository;

        public CategoryService()
        {
            _genericRepository = new GenericRepository<ExpenseCategory>();
        }

        public async Task<IEnumerable<ExpenseCategory>> GetAsync()
        {
            return await _genericRepository.GetAsync();
        }

        public async Task<ExpenseCategory> GetByIdAsync(int id)
        {
            return await _genericRepository.GetByIdAsync(id);
        }

    }
}
