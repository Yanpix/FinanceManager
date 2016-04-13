using System;
using Windows.Phone.UI.Input;

namespace YanpixFinanceManager.ViewModel.Common
{
    public class PlatformEvents : IPlatformEvents
    {
        public event EventHandler BackButtonPressed;

        public PlatformEvents()
        {
            HardwareButtons.BackPressed += HardwareButtons_BackPressed;
        }

        void HardwareButtons_BackPressed(object sender, BackPressedEventArgs e)
        {
            if (BackButtonPressed != null)
            {
                BackButtonPressed(this, new EventArgs());
                e.Handled = true;
            }
        }
    }
}
