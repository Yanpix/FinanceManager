using System.Collections.ObjectModel;
using System.Threading.Tasks;
using FinanceManager.BLL.IService;
using FinanceManager.DAL.Models;
using FinanceManager.DAL.UnitOfWork;

namespace FinanceManager.BLL.Service
{
    public class CurrencyService : ICurrencyService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CurrencyService()
        {
            _unitOfWork = new UnitOfWork();
        }

        public async Task<ObservableCollection<Currency>> GetAsync()
        {
            return await _unitOfWork.Repository<Currency>().GetAsync();
        }

        public async Task<Currency> GetByIdAsync(int id)
        {
            return await _unitOfWork.Repository<Currency>().GetByIdAsync(id);
        }

        public async Task CreateAsync(string name)
        {
            
            var model = new Currency
            {
                Name = name,
                Value = 10
            };

            await _unitOfWork.Repository<Currency>().CreateAsync(model);
        }

        public async Task DeleteAsync(int id)
        {
            await _unitOfWork.Repository<Currency>().DeleteAsync(id);
        }

        public async Task DeleteAllAsync()
        {
            await _unitOfWork.Repository<Currency>().DeleteAll();
        }
    }
}
