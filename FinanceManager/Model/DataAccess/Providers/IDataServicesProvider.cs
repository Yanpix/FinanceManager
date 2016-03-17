using FinanceManager.Model.DataAccess.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManager.Model.DataAccess.Providers
{
    public interface IDataServicesProvider
    {
        IDataService<T> Get<T>() where T : class, new();
    }
}
