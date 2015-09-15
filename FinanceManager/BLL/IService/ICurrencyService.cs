using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using FinanceManager.DAL.Models;

namespace FinanceManager.BLL.IService
{
    public interface ICurrencyService
    {
        Task<ObservableCollection<Currency>> GetAsync();
        Task<Currency> GetByIdAsync(int id);
        Task CreateAsync(Currency currency);
        Task DeleteAsync(int id);
        Task DeleteAllAsync();
    }
}