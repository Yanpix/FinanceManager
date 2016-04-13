using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YanpixFinanceManager.Model.Entities;

namespace YanpixFinanceManager.ViewModel.Common
{
    public class CurrencyRate : BindableBase
    {
        private Currency _currency;

        public Currency Currency
        {
            get
            {
                if (_currency == null)
                    _currency = new Currency();

                return _currency;
            }
            set
            {
                _currency = value;
                OnPropertyChanged();
            }
        }

        private string _value;

        public string Value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
                OnPropertyChanged();
            }
        }
    }
}
