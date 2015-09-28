using FinanceManager.BLL.IService;
using FinanceManager.BLL.Service;
using FinanceManager.DAL.UnitOfWork;
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
        public CategoriesViewModel CategoriesViewModel => _container.Resolve<CategoriesViewModel>();
        public INavigationService NavigationService => _container.Resolve<NavigationService>();
        public IUnitOfWork UnitOfWork => _container.Resolve<UnitOfWork>();
        public ICurrencyService CurrencyService => _container.Resolve<CurrencyService>();
        public ICategoriesService CategoriesService => _container.Resolve<CategoriesService>();
        public void Initializate()
        {
            _container.RegisterType<MainViewModel>();
            _container.RegisterType<ICurrencyService, CurrencyService>();
            _container.RegisterType<IUnitOfWork, UnitOfWork>();
            _container.RegisterType<INavigationService, NavigationService>();
            _container.RegisterType<ICategoriesService, CategoriesService>();
        }

    }
}
