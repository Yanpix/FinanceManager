
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
       public MainViewModel MainViewModel
        {
            get
            {
                var Container = new UnityContainer();
                Container.RegisterType(typeof(MainViewModel));
                return Container.Resolve<MainViewModel>();
            }
        } 
        
    }
}
