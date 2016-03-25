using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace FinanceManager.View.Converters
{
    class SelectedToForegroundConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            bool isSelected = (bool)value;

            Color color = isSelected ? ((SolidColorBrush)Application.Current.Resources["PhoneAccentBrush"]).Color : Colors.White;

            return new SolidColorBrush { Color = color };
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
