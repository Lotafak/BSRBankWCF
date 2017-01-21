using System;
using System.Text;
using System.Windows;
using Client.Properties;
using Client.ServiceReference1;

namespace Client.Utils
{
    /// <summary>
    /// Class providing utilities for client application
    /// </summary>
    public static class ClientUtils
    {
        /// <summary>
        /// ShowMessage specified for showing <see cref="Message"/> class objects
        /// </summary>
        /// <param name="message"><see cref="ErrorMessage"/> or <see cref="ResultMessage"/> object</param>
        public static void ShowMessage(Message message)
        {
            MessageBox.Show(message.MessageText,
                message.IsError ? Resources.ErrorCaptionText : Resources.InfoCaptionText,
                MessageBoxButton.OK,
                message.IsError ? MessageBoxImage.Error : MessageBoxImage.Information);
        }
    }
}