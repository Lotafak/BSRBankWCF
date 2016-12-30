using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json;

namespace BSRBankWCF
{
    public class AccountUtils
    {
        public static bool ValidateAccountNumber(string acc)
        {
            if (acc.Length != 26) return false;
            var check = acc.Substring(2) + acc.Substring(0, 2);
            var intarr = new string[4];
            check = check.TrimStart('0');
            for (var i = 0; i < intarr.Length; i++)
            {
                if (i*6 + 6 < check.Length)
                    intarr[i] = check.Substring(i*6, 6);
                else
                    intarr[i] = check.Substring(i*6);
            }
            for (var i = 0; i < intarr.Length; i++)
            {
                var mod = int.Parse(intarr[i])%97;
                if (i >= intarr.Length - 1)
                {
                    return mod == 1;
                }
                var next = intarr[i + 1];
                var nextString = mod + next;
                intarr[i + 1] = nextString;
            }
            return false;
        }

        public static string CreateAccountNumber(string bankId, int id)
        {
            if (bankId.Length != 8) return "";

            var accountIdWithZeros = CreateAccountId(id);
            var check = bankId + accountIdWithZeros + "00";
            var intarr = new string[4];
            check = check.TrimStart('0');
            for (var i = 0; i < intarr.Length; i++)
            {
                if (i*6 + 6 < check.Length)
                    intarr[i] = check.Substring(i*6, 6);
                else
                    intarr[i] = check.Substring(i*6);
            }
            for (var i = 0; i < intarr.Length; i++)
            {
                var mod = int.Parse(intarr[i])%97;
                if (i >= intarr.Length - 1)
                {
                    var chksum = 98 - mod;
                    var strchcksum = "";
                    if (chksum.ToString().Length == 1)
                        strchcksum = "0";
                    strchcksum += chksum;
                    return strchcksum + bankId + accountIdWithZeros;
                }
                var next = intarr[i + 1];
                var nextString = mod + next;
                intarr[i + 1] = nextString;
            }
            return "";
        }

        public static string CreateAccountId(int id)
        {
            var sb = new StringBuilder();
            for (var i = 0; i < 16 - id.ToString().Length; i++)
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

        public static string[] LoginAndPasswordFromCredentials(string base64Credentials)
        {
            return Base64Decode(base64Credentials).Split(':');
        }

        public static MemoryStream CreateJsonErrorResponse(string message)
        {
            return new MemoryStream(Encoding.UTF8.GetBytes(JsonConvert.
                SerializeObject(new Dictionary<string, string> { { "error", message } }, Formatting.None)));
        }
    }
}