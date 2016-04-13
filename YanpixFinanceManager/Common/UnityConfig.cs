using Microsoft.Practices.Unity;
using YanpixFinanceManager.Model.DataAccess.Services;
using YanpixFinanceManager.Model.DataAccess.UnitOfWork;
using YanpixFinanceManager.ViewModel.Common;

namespace YanpixFinanceManager.Common
{
    public class UnityConfig
    {
        #region Unity Container

        private static IUnityContainer _container;

        public static IUnityContainer Container
        {
            get
            {
                if (_container == null)
                {
                    _container = new UnityContainer();
                    RegisterTypes(_container);
                }
                return _container;
            }
        }

        #endregion

        private static void RegisterTypes(IUnityContainer container)
        {
            container.RegisterType<IUnitOfWork, UnitOfWork>();

            container.RegisterType(typeof(IEntityBaseService<>), typeof(EntityBaseService<>));

            container.RegisterType<INavigationService, NavigationService>();

            container.RegisterType<IPlatformEvents, PlatformEvents>();

            container.RegisterType<IDataInitService, DataInitService>();

            container.RegisterType<ICurrencyRatesService, CurrencyRatesService>();
        }
    }
}
