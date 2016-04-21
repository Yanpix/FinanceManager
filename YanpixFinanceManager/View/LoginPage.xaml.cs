using YanpixFinanceManager.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Display;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using YanpixFinanceManager.View.Common;
using YanpixFinanceManager.ViewModel;
using YanpixFinanceManager.ViewModel.Common;
using Microsoft.Practices.Unity;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace YanpixFinanceManager.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LoginPage : Page
    {
        public LoginPage()
        {
            this.InitializeComponent();

            this.DataContext = App.container.Resolve<LoginViewModel>();
        }
    }
}
