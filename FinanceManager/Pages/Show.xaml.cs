using System;
using System.Collections.ObjectModel;
using System.Linq;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using FinanceManager.BLL.IService;
using FinanceManager.BLL.Service;
using FinanceManager.DAL.Models;

namespace FinanceManager.Pages
{

    public sealed partial class Show
    {
        private readonly ICurrencyService _currencyService;
        ObservableCollection<Currency> model = new ObservableCollection<Currency>();

        public Show()
        {
            _currencyService = new CurrencyService();
    
            InitializeComponent();
            Loaded += Show_Loaded;
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private async void Show_Loaded(object sender, RoutedEventArgs e)
        {
            model = await _currencyService.GetAsync();
            if (model.Count > 0)
            {
                Btn_Delete.IsEnabled = true;
            }
            listBoxobj.ItemsSource = model.OrderByDescending(i => i.Id).ToList();
        }

        private  void Add_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }

        private async void DeleteAll_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new MessageDialog("Are you sure you want to remove all your data ?");
            dialog.Commands.Add(new UICommand("No", Command));
            dialog.Commands.Add(new UICommand("Yes", Command));
            await dialog.ShowAsync();
        }

        private void Command(IUICommand command)
        {
            if (command.Label.Equals("Yes"))
            {
                _currencyService.DeleteAllAsync();
                model.Clear();
                Btn_Delete.IsEnabled = false;
                listBoxobj.ItemsSource = model;
            }
        }

        private void listBoxobj_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listBoxobj.SelectedIndex != -1)
            {
                var listitem = listBoxobj.SelectedItem as Currency;//Get slected listbox item contact ID 
                Frame.Navigate(typeof(UpdateOrDelete), listitem.Id);
            }
        }
    }
}
