using FinanceManager.BLL.IService;
using FinanceManager.BLL.Service;
using FinanceManager.DAL.UnitOfWork;
using Ninject;

namespace FinanceManager.Infrastructure
{
    public static class NinjectContainer
    {
        private static readonly IKernel Kernel = new StandardKernel();

        public static void Initialize()
        {
            Kernel.Bind<ICurrencyService>().To<CurrencyService>();
            Kernel.Bind<IUnitOfWork>().To<UnitOfWork>();
        }

        public static T Get<T>()
        {
            return Kernel.Get<T>();
        }
    }
}
