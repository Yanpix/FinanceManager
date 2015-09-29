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
        Task<ObservableCollection<Category>> GetAllCategoriesAsync();
        Task CreateCategoryAsync(Category category);
        Task DeleteCategoryAsync(int id);
        Task DeleteAllCategories();

        
        ObservableCollection<Category> GetAllCategories();
        void CreateCategory(Category category);
        void DeleteCategory(int id);

    }

}
