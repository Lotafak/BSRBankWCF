using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BSRBankWCF.Models
{
    [DataContract]
    public class Account
    {
        [DataMember]
        public string BankAccountNumber { get; set; }
        [DataMember]
        public long Amount { get; set; }
    }
}
