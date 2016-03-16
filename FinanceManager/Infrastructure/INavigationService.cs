using System;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace FinanceManager.Infrastructure
{
    /// <summary>
    /// Navigation service interface for performing navigation between pages
    /// </summary>
    public interface INavigationService
    {
        bool Navigate(Type type);
        bool Navigate(Type type, object[] parameter);
        object[] GetNavigationData();
        object GetNavigationData(int i);
        void GoBack();
    }
}
