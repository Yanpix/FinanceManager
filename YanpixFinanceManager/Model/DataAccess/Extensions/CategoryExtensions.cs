using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YanpixFinanceManager.Model.DataAccess.Services;
using YanpixFinanceManager.Model.Entities;
using YanpixFinanceManager.Model.Entities.Enums;

namespace YanpixFinanceManager.Model.DataAccess.Extensions
{
    public static class CategoryExtensions
    {
        public static List<Category> GetExpenceCategories(this IEntityBaseService<Category> categoryService)
        {
            return categoryService.GetAll()
                .Where(c => c.Type == TransactionType.Expence)
                .OrderBy(o => o.Title)
                .ToList();
        }

        public static List<Category> GetIncomeCategories(this IEntityBaseService<Category> categoryService)
        {
            return categoryService.GetAll()
                .Where(c => c.Type == TransactionType.Income)
                .OrderBy(o => o.Title)
                .ToList();
        }

        public static void DeleteAllIncomeCategories(this IEntityBaseService<Category> categoryService)
        {
            var incomeCategories = categoryService.GetIncomeCategories();

            foreach (Category category in incomeCategories)
            {
                categoryService.Delete(category.Id);
            }
        }

        public static void DeleteAllExpenceCategories(this IEntityBaseService<Category> categoryService)
        {
            var expenceCategories = categoryService.GetExpenceCategories();

            foreach (Category category in expenceCategories)
            {
                categoryService.Delete(category.Id);
            }
        }

        public static Category ExistingCategory(this IEntityBaseService<Category> categoryService, string title, int categoryType)
        {
            return categoryService.GetAll()
                .Where(c => c.Title == title && c.Type == (TransactionType)Enum.Parse(typeof(TransactionType), categoryType.ToString()))
                .FirstOrDefault();
        }
    }
}
