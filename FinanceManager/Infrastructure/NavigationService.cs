using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace FinanceManager.Infrastructure
{
    /// <summary>
    /// Navigation service for performing navigation between pages
    /// </summary>
    public class NavigationService : INavigationService
    {
        public bool Navigate(Type type, object parameter)
        {
            return ((Frame)Window.Current.Content).Navigate(type, parameter);
        }

        public bool Navigate(Type type)
        {
            return ((Frame)Window.Current.Content).Navigate(type);
        }

        public void GoBack()
        {
            if (((Frame)Window.Current.Content).CanGoBack)
                ((Frame)Window.Current.Content).GoBack();
        }
    }
}
