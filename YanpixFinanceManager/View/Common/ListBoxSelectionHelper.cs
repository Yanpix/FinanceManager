using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;

namespace YanpixFinanceManager.View.Common
{
    public class ListBoxSelectionHelper
    {
        public static string GetIsSelectedContinerBinding(DependencyObject obj)
        {
            return (string)obj.GetValue(IsSelectedContinerBindingProperty);
        }

        public static void SetIsSelectedContinerBinding(DependencyObject obj, string value)
        {
            obj.SetValue(IsSelectedContinerBindingProperty, value);
        }

        // Using a DependencyProperty as the backing store for IsSelectedContinerBinding.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsSelectedContinerBindingProperty =
            DependencyProperty.RegisterAttached("IsSelectedContinerBinding", typeof(string), typeof(ListBoxSelectionHelper), new PropertyMetadata(null, IsSelectedContinerBindingPropertyChangedCallback));


        public static void IsSelectedContinerBindingPropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            BindingOperations.SetBinding(d, ListBoxItem.IsSelectedProperty, new Binding()
            {
                Source = d,
                Path = new PropertyPath("Content." + e.NewValue),
                Mode = BindingMode.TwoWay
            });
        }
    }
}
