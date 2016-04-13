using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace YanpixFinanceManager.View.Common.Converters
{
    public class DecimalConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return value.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            decimal number;

            if (decimal.TryParse(value.ToString(), out number))
            {
                return number;
            }
            else
            {
                return null;
            }
        }
    }
}
