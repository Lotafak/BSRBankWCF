using System;
using System.Text;

namespace Client.Utils
{
    public class AccountUtils
    {
        public static bool ValidateAccountNumber( string input )
        {
            if ( input.Length != 26 && input.Length != 32 ) return false;

            input = input.Replace(" ", String.Empty);

            string checkSum = input.Substring(0, 2);
            string accountNumber = input.Substring(2);
            const int countryCode = 2521;
            string reversedDigits = accountNumber + countryCode + checkSum;
            int controlNum = ModString(reversedDigits, 97);

            return ( controlNum == 1 );

        }

        static int ModString( string x, int y )
        {
            if ( x.Length == 0 )
                return 0;
            string x2 = x.Substring(0, x.Length - 1); // first digits
            int x3 = int.Parse(x.Substring(x.Length - 1));   // last digit
            return ( ModString(x2, y) * 10 + x3 ) % y;
        }

        public static string CreateAccountNumber( string bankId, int id )
        {
            var account = CreateBankAccountNumberFromId(id);
            var stringAcc = bankId + account;
            var x = "00";
            var corrAcc = "";
            while ( !ValidateAccountNumber(corrAcc) )
            {
                corrAcc = x + stringAcc;
                var chksum = int.Parse(x) + 1;
                if ( chksum < 10 )
                    x = "0" + chksum;
                else
                    x = chksum.ToString();
            }
            return corrAcc;
        }

        public static string CreateBankAccountNumberFromId( int id )
        {
            var sb = new StringBuilder();
            for ( var i = 0; i < 16 - id.ToString().Length; i++ )
                sb.Append(0);
            sb.Append(id);
            return sb.ToString();
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

        public static string LoginFromCredentials(string credentials)
        {
            return Base64Decode(credentials).Split(':')[0];
        }
    }
}