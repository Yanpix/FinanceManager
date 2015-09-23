using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using FinanceManager.Base;
using FinanceManager.BLL.IService;
using FinanceManager.BLL.Service;
using FinanceManager.DAL.UnitOfWork;
using FinanceManager.Pages;
using Microsoft.Practices.Unity;


namespace FinanceManager.ViewModel
{
    public class MainViewModel : BindableBase
    {
        private readonly INavigationService _navigationService;

        public MainViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            TEST = "TEST!";
        }

        public string TEST { get; set; }

        public RelayCommand Move => new RelayCommand(GoToShow);

        public void GoToShow()
        {
            _navigationService.Navigate(typeof (Show));
        }
    }
}
