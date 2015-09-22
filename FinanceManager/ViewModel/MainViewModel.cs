using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinanceManager.Base;
using FinanceManager.DAL.UnitOfWork;
using Microsoft.Practices.Unity;


namespace FinanceManager.ViewModel
{
    public class MainViewModel : BindableBase
    {
        private IUnityContainer _container;

        public IUnitOfWork UnitOfWork { get; set; }

        public MainViewModel()
        {
            
            TEST = "TEST!";
        }

        public string TEST { get; set; }

    }
}
