using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace YanpixFinanceManager.View.Common.Converters
{
    public class DateTimeToStringConverter : DependencyObject, IValueConverter
    {
        public bool IsMonthPeriod
        {
            get { return (bool)GetValue(_isMonthPeriod); }
            set { SetValue(_isMonthPeriod, value); }
        }

        public static readonly DependencyProperty _isMonthPeriod =
            DependencyProperty.Register("IsMonthPeriod",
                typeof(bool),
                typeof(DateTimeToStringConverter),
                new PropertyMetadata(null));

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            DateTime period = (DateTime)value;

            if (IsMonthPeriod)
                return period.ToString("MMMM yyyy", CultureInfo.InvariantCulture);
            else
                return period.ToString("yyyy", CultureInfo.InvariantCulture);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
