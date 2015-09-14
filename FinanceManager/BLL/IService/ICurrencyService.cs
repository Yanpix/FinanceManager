using System.Collections.Generic;
using System.Threading.Tasks;
using FinanceManager.DAL.Models;

namespace FinanceManager.BLL.IService
{
    public interface ICurrencyService
    {
        Task<IEnumerable<Currency>> GetAsync();
    }
}