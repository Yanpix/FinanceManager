using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace YanpixFinanceManager.View.Common.Converters
{
    public class WidthScaleConverter : DependencyObject, IValueConverter
    {
        public double ScaleFactor
        {
            get { return (double)GetValue(_scaleFactor); }
            set { SetValue(_scaleFactor, value); }
        }

        public static readonly DependencyProperty _scaleFactor =
            DependencyProperty.Register("ScaleFactor",
                typeof(double),
                typeof(WidthScaleConverter),
                new PropertyMetadata(null));

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value != null)
                return (double)value * ScaleFactor;
            else
                return 0.0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
