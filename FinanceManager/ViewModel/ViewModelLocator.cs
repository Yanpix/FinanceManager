using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinanceManager.BLL.IService;
using FinanceManager.BLL.Service;
using FinanceManager.DAL.UnitOfWork;
using Ninject;

namespace FinanceManager.ViewModel
{
    public class ViewModelLocator
    {

        public ICurrencyService NavigationService => NinjectContainer.Get<ICurrencyService>();
        public MainViewModel MainViewModel => NinjectContainer.Get<MainViewModel>();
    }


    public class NinjectContainer
    {
        private static readonly IKernel Kernel = new StandardKernel();

        public static void Initialize()
        {
            Kernel.Bind<IUnitOfWork>().To<UnitOfWork>();
            Kernel.Bind<ICurrencyService>().To<CurrencyService>();
        }

        public static T Get<T>()
        {
            return Kernel.Get<T>();
        }
    }
}
