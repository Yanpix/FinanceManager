using System.Collections.Generic;
using System.Threading.Tasks;
using FinanceManager.BLL.IService;
using FinanceManager.DAL.Models;
using FinanceManager.DAL.UnitOfWork;

namespace FinanceManager.BLL.Service
{
    public class CurrencyService : ICurrencyService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CurrencyService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Currency>> GetAsync()
        {
            return await _unitOfWork.Repository<Currency>().GetAsync();
        }
    }
}
