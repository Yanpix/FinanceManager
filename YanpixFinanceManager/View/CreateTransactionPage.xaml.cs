using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using YanpixFinanceManager.ViewModel;
using Microsoft.Practices.Unity;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace YanpixFinanceManager.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CreateTransactionPage : Page
    {
        public CreateTransactionPage()
        {
            this.InitializeComponent();

            this.DataContext = App.container.Resolve<CreateTransactionViewModel>();
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private void Flyout_Opened(object sender, object e)
        {
            var vm = (CreateTransactionViewModel)DataContext;

            vm.IsCalculatorOpened = true;
            vm.IsCalculatorClosed = false;
        }

        private void Flyout_Closed(object sender, object e)
        {
            var vm = (CreateTransactionViewModel)DataContext;

            vm.IsCalculatorOpened = false;
            vm.IsCalculatorClosed = true;
        }
    }
}
