using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinanceManager.Base;
using FinanceManager.DAL.UnitOfWork;

namespace FinanceManager.ViewModel
{
   public class MainViewModel : BindableBase
    {
        public IUnitOfWork UnitOfWork { get; set; }

    }
}
