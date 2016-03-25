using System;
using System.Collections.Generic;
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
        bool Navigate(Type type, Dictionary<string, object> data);
        Dictionary<string, object> GetNavigationData();
        object GetNavigationData(string key);
        void GoBack();
    }
}
