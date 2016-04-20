using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using YanpixFinanceManager.Model.Entities.Enums;

namespace YanpixFinanceManager.View.Common.Converters
{
    public class TransactionTypeToSymbolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            TransactionType type = (TransactionType)value;

            if (type == 0)
                return "Add";
            else
                return "Remove";
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
