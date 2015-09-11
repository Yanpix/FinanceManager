using System.Collections.Generic;
using System.Threading.Tasks;
using FinanceManager.DAL.Models;
using FinanceManager.DAL.Repository;

namespace FinanceManager.BLL.Service
{
    public class CurrencyService
    {
        private readonly IGenericRepository<Currency> _genericRepository;

        public CurrencyService()
        {
            _genericRepository = new GenericRepository<Currency>();
        }

        public async Task<IEnumerable<Currency>> GetAsync()
        {
            return await _genericRepository.GetAsync();
        }
    }
}
