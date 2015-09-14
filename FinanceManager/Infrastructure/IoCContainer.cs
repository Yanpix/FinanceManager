using FinanceManager.DAL.UnitOfWork;
using Ninject;

namespace FinanceManager.Infrastructure
{
    public static class IoCContainter
    {
        private static readonly IKernel Kernel = new StandardKernel();

        public static void Initialize()
        {
            Kernel.Bind<IUnitOfWork>().To<UnitOfWork>();
        }

        public static T Get<T>()
        {
            return Kernel.Get<T>();
        }
    }
}
