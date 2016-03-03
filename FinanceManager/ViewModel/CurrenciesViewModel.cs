using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Web.Http.Headers;
using FinanceManager.Base;
using FinanceManager.DAL.Models;

namespace FinanceManager.ViewModel
{
    public class CurrenciesViewModel : BindableBase
    {
        public List<Currency> Currencies
        {

            get
            {
                List <Currency> test = new List<Currency>();
                test.Add(new Currency
                {
                    Name = "Grivna",
                    //CurrencyId = 1,
                    //Value = 1,
                    Symbol = "₴"
                });
                test.Add(new Currency
                {
                    Name = "Rubas",
                    //CurrencyId = 2,
                    //Value = 100/33,
                    Symbol = "₴"
                });

                return test;
            }
        }

    }
    
}
