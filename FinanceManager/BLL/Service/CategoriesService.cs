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
        public async Task<ObservableCollection<Category>> GetAllCategoriesAsync()
        {
            return await _unitOfWork.Repository<Category>().GetAsync();
        }

        public async Task CreateCategoryAsync(Category category)
        {
            await _unitOfWork.Repository<Category>().CreateAsync(category);
        }
        
        public async Task DeleteCategoryAsync(int id)
        {
          await _unitOfWork.Repository<Category>().DeleteAsync(id);
        }


        public async Task DeleteAllCategories()
        {
            await _unitOfWork.Repository<Category>().DeleteAll();
        }

        public ObservableCollection<Category> GetAllCategories()
        {
            return _unitOfWork.Repository<Category>().Get();
        }

 

        public void CreateCategory(Category category)
        {
            _unitOfWork.Repository<Category>().Create(category);
        }

        public void DeleteCategory(int id)
        {
            _unitOfWork.Repository<Category>().DeleteAsync(id);
        }


    }
}
