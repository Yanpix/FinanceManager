using System.Runtime.InteropServices.ComTypes;
using FinanceManager.BLL.IService;
using FinanceManager.BLL.Service;
using FinanceManager.DAL.UnitOfWork;
using FinanceManager.ViewModel;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;


namespace FinanceManager.Infrastructure
{
    public class IoCContainer
    {
        private readonly UnityContainer _container;

        public IoCContainer(UnityContainer container)
        {
            
            _container = container;
            Initializate();

        }
        public INavigationService NavigationService => _container.Resolve<INavigationService>();

        public void Initializate()
        {
            _container.RegisterType<ICurrencyService, CurrencyService>();
            _container.RegisterType<IUnitOfWork, UnitOfWork>();
            _container.RegisterType<INavigationService, NavigationService>();
        }


    }
}
