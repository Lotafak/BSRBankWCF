using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json;

namespace BSRBankWCF.Utils
{
    public class AccountUtils
    {
        /// <summary>
        /// Validate NRB bank account number
        /// </summary>
        /// <param name="input">26 or 32 digit NRB format bank account number</param>
        /// <returns>True if valid</returns>
        public static bool ValidateAccountNumber(string input)
        {
            if (input.Length != 26 && input.Length != 32) return false;

            input = input.Replace(" ", String.Empty);

            var checkSum = input.Substring(0, 2);
            var accountNumber = input.Substring(2);
            const int countryCode = 2521;
            var reversedDigits = accountNumber + countryCode + checkSum;
            var controlNum = ModString(reversedDigits, 97);

            return (controlNum == 1);

        }

        private static int ModString(string x, int y)
        {
            if (x.Length == 0)
                return 0;
            var x2 = x.Substring(0, x.Length - 1); // first digits
            var x3 = int.Parse(x.Substring(x.Length - 1));   // last digit
            return (ModString(x2, y) * 10 + x3) % y;
        }

        /// <summary>
        /// Creates valid 26-digit bank account number in NRB format
        /// </summary>
        /// <param name="bankId">8-digit bank Id number (i.e. 00109562) </param>
        /// <param name="id">Account number for given bank (Last number(s))</param>
        /// <returns>New bank account number</returns>
        public static string CreateAccountNumber(string bankId, int id)
        {
            var account = CreateBankAccountNumberFromId(id);
            var stringAcc = bankId + account;
            var x = "00";
            var corrAcc = "";
            while (!ValidateAccountNumber(corrAcc))
            {
                corrAcc = x + stringAcc;
                var chksum = int.Parse(x) + 1;
                if (chksum < 10)
                    x = "0" + chksum;
                else
                    x = chksum.ToString();
            }
            return corrAcc;
        }

        /// <summary>
        /// Creates 16-digit string with account id number
        /// </summary>
        /// <param name="id">Last number(s) of account id</param>
        /// <returns>16-digit string number</returns>
        public static string CreateBankAccountNumberFromId(int id)
        {
            var sb = new StringBuilder();
            for (var i = 0; i < 16 - id.ToString().Length; i++)
                sb.Append(0);
            sb.Append(id);
            return sb.ToString();
        }

        /// <summary>
        /// Encode credentials to Base64 format
        /// </summary>
        /// <param name="plainText">string to encode</param>
        /// <returns>Base64 encoded string</returns>
        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            return Convert.ToBase64String(plainTextBytes);
        }

        /// <summary>
        /// Decode Base64 encoded string
        /// </summary>
        /// <param name="base64EncodedData">Base64 encoded string</param>
        /// <returns>Decoded string</returns>
        public static string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = Convert.FromBase64String(base64EncodedData);
            return Encoding.UTF8.GetString(base64EncodedBytes);
        }

        /// <summary>
        /// Creates REST service response when somethings went wrong. 
        /// </summary>
        /// <param name="message">Message to return to service caller</param>
        /// <returns>Stream with response in Json format</returns>
        public static MemoryStream CreateJsonErrorResponse(string message)
        {
            return new MemoryStream(Encoding.UTF8.GetBytes(JsonConvert.
                SerializeObject(new Dictionary<string, string> { { "error", message } }, Formatting.None)));
        }

        public static string LoginFromCredentials( string credentials )
        {
            return Base64Decode(credentials).Split(':')[0];
        }
    }
}