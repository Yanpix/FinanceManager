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
        private static object[] _navigationData = null;

        public bool Navigate(Type type, object[] parameter)
        {
            _navigationData = parameter;
            return ((Frame)Window.Current.Content).Navigate(type);
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

        public object[] GetNavigationData()
        {
            return _navigationData;
        }

        public object GetNavigationData(int i)
        {
            return _navigationData[i];
        }
    }
}
