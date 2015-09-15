using System;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using FinanceManager.BLL.IService;
using FinanceManager.BLL.Service;

namespace FinanceManager.Pages
{

    public sealed partial class Show : Page
    {
        private readonly ICurrencyService _currencyService;

        public Show()
        {
            _currencyService = new CurrencyService();
    
            InitializeComponent();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        //private void ReadContactList_Loaded(object sender, RoutedEventArgs e)
        //{
        //    DB_ContactList = _currencyService.GetAsync(); 
        //    if (DB_ContactList.Count > 0)
        //    {
        //        Btn_Delete.IsEnabled = true;
        //    }
        //    listBoxobj.ItemsSource = DB_ContactList.OrderByDescending(i => i.Id).ToList();//Binding DB data to LISTBOX and Latest contact ID can Display first.   
        //}

        private  void Add_Click(object sender, RoutedEventArgs e)
        {
           
        }

        private void DeleteAll_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void listBoxobj_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
