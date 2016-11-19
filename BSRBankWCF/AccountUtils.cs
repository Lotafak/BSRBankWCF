﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSRBankWCF
{
    public class AccountUtils
    {
        public static bool ValidateAccountNumber( string acc )
        {
            if ( acc.Length != 26 ) return false;
            var check = acc.Substring(2) + acc.Substring(0, 2);
            var intarr = new int[4];
            check = check.TrimStart('0');
            for ( var i = 0; i < intarr.Length; i++ )
            {
                if ( i * 6 + 6 < check.Length )
                    intarr[i] = int.Parse(check.Substring(i * 6, 6));
                else
                    intarr[i] = int.Parse(check.Substring(i * 6));
            }
            for ( var i = 0; i < intarr.Length; i++ )
            {
                var mod = intarr[i] % 97;
                if ( i >= intarr.Length - 1 )
                {
                    return mod == 1;
                }
                var next = intarr[i + 1];
                var nextString = mod + next.ToString();
                intarr[i + 1] = int.Parse(nextString);
            }
            return false;
        }

        public static string CreateAccountNumber( string bankId, int id )
        {
            if ( bankId.Length != 8 ) return "";

            var accountIdWithZeros = createAccountId(id);
            var check = bankId + accountIdWithZeros + "00";
            var intarr = new int[4];
            check = check.TrimStart('0');
            for ( var i = 0; i < intarr.Length; i++ )
            {
                if ( i * 6 + 6 < check.Length )
                    intarr[i] = int.Parse(check.Substring(i * 6, 6));
                else
                    intarr[i] = int.Parse(check.Substring(i * 6));
            }
            for ( var i = 0; i < intarr.Length; i++ )
            {
                var mod = intarr[i] % 97;
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
                var nextString = mod + next.ToString();
                intarr[i + 1] = int.Parse(nextString);
            }
            return "";

        }

        public static string createAccountId(int id)
        {
            var sb = new StringBuilder();
            for (var i = 0; i < 16 - id.ToString().Length; i++)
                sb.Append(0);
            sb.Append(id);
            return sb.ToString();
        }
    }
}