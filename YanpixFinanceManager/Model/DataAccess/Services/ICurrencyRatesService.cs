using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YanpixFinanceManager.Model.DataAccess.Services
{
    public interface ICurrencyRatesService
    {
        Task UpdateExchangeRates();

        decimal Exchange(string from, string to, decimal amount);

        decimal GetExchangeCoeff(string from, string to);
    }
}
