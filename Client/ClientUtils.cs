using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Client.ServiceReference1;

namespace Client
{
    public static class ClientUtils
    {
        public static void ShowMessage( Message message)
        {
            MessageBox.Show(message.MessageText,
                message.IsError ? Properties.Resources.ErrorCaptionText : Properties.Resources.InfoCaptionText,
                MessageBoxButton.OK,
                message.IsError ? MessageBoxImage.Error : MessageBoxImage.Information);
        }
    }
}
