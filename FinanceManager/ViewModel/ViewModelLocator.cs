
using System.Linq.Expressions;
using FinanceManager.BLL.IService;
using FinanceManager.BLL.Service;
using FinanceManager.DAL.UnitOfWork;
using FinanceManager.Infrastructure;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;

namespace FinanceManager.ViewModel
{
    public class ViewModelLocator
    {
        private readonly UnityContainer _container;

        public ViewModelLocator()
        {
            _container = new UnityContainer();
            Initializate();
        }
        public MainViewModel MainViewModel => _container.Resolve<MainViewModel>();
        public INavigationService NavigationService => _container.Resolve<NavigationService>();

        public void Initializate()
        {
            _container.RegisterType<MainViewModel>();
            _container.RegisterType<ICurrencyService, CurrencyService>();
            _container.RegisterType<IUnitOfWork, UnitOfWork>();
            _container.RegisterType<INavigationService, NavigationService>();
        }

    }
}
