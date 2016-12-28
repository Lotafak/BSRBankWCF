using System;
using System.Text;
using System.Windows;
using Client.Properties;
using Client.ServiceReference1;

namespace Client
{
    public static class ClientUtils
    {
        public static void ShowMessage(Message message)
        {
            MessageBox.Show(message.MessageText,
                message.IsError ? Resources.ErrorCaptionText : Resources.InfoCaptionText,
                MessageBoxButton.OK,
                message.IsError ? MessageBoxImage.Error : MessageBoxImage.Information);
        }

        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            return Convert.ToBase64String(plainTextBytes);
        }

        public static string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = Convert.FromBase64String(base64EncodedData);
            return Encoding.UTF8.GetString(base64EncodedBytes);
        }
    }
}