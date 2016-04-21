using System;
using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using YanpixFinanceManager.View;

namespace YanpixFinanceManager.ViewModel.Common
{
    /// <summary>
    /// Navigation service for performing navigation between pages
    /// </summary>
    public class NavigationService : INavigationService
    {
        private static Dictionary<string, object> _navigationData = new Dictionary<string, object>();

        public bool Navigate(Type type, Dictionary<string, object> data)
        {
            foreach (var dataItem in data)
            {
                if (_navigationData.ContainsKey(dataItem.Key))
                {
                    _navigationData[dataItem.Key] = dataItem.Value;
                }
                else
                    _navigationData.Add(dataItem.Key, dataItem.Value);
            }
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

        public Dictionary<string, object> GetNavigationData()
        {
            Dictionary<string, object> data = _navigationData;

            return data;
        }

        public object GetNavigationData(string key)
        {
            object data;

            if (_navigationData.ContainsKey(key))
            {
                data = _navigationData[key];
                return data;
            }
            return null;
        }

        public void ClearNavigationData(string key)
        {
            if (_navigationData.ContainsKey(key))
                _navigationData.Remove(key);
        }
    }
}
