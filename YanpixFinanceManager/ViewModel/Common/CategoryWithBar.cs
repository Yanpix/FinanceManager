using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YanpixFinanceManager.Model.Entities.Enums;

namespace YanpixFinanceManager.ViewModel.Common
{
    public class CategoryWithBar : BindableBase
    {
        private string _image;

        public string Image
        {
            get
            {
                return _image;
            }
            set
            {
                _image = value;
                OnPropertyChanged();
            }
        }

        private string _currencySymbol;

        public string CurrencySymbol
        {
            get
            {
                return _currencySymbol;
            }
            set
            {
                _currencySymbol = value;
                OnPropertyChanged();
            }
        }

        private TransactionType _type;

        public TransactionType Type
        {
            get
            {
                return _type;
            }
            set
            {
                _type = value;
                OnPropertyChanged();
            }
        }

        private decimal _value;

        public decimal Value
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

        private decimal _total;

        public decimal Total
        {
            get
            {
                return _total;
            }
            set
            {
                _total = value;
                OnPropertyChanged();
                InitScaleFactors();
            }
        }

        public decimal Persent
        {
            get
            {
                if (Total.CompareTo(0M) == 1)
                    return Value / Total * 100M;
                else
                    return 0M;
            }
        }

        private string _title;

        public string Title
        {
            get
            {
                return _title;
            }
            set
            {
                _title = value;
                OnPropertyChanged();
            }
        }

        private bool _isPositive;

        public bool IsPositive
        {
            get
            {
                return _isPositive;
            }
            set
            {
                _isPositive = value;
                OnPropertyChanged();
            }
        }

        private double _valueScaleFactor;

        public double ValueScaleFactor
        {
            get
            {
                return _valueScaleFactor;
            }
            set
            {
                _valueScaleFactor = value;
                OnPropertyChanged();
            }
        }

        private double _persentScaleFactor;

        public double PersentScaleFactor
        {
            get
            {
                return _persentScaleFactor;
            }
            set
            {
                _persentScaleFactor = value;
                OnPropertyChanged();
            }
        }

        private void InitScaleFactors()
        {
            _valueScaleFactor = (double)(Persent / 100M);
            _persentScaleFactor = 1 - _valueScaleFactor;

            IsPositive = Type == TransactionType.Income;
        }
    }
}
