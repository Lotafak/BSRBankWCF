using System.IO;
using System.Net.Http;
using System.ServiceModel;
using System.ServiceModel.Web;
using BSRBankWCF.Models;

namespace BSRBankWCF.Services
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IRestService" in both code and config file together.
    [ServiceContract]
    public interface IRestService
    {
        [OperationContract]
        [WebInvoke(Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            UriTemplate = "{bankAccountNumber}")]
        Stream RecieveTransfer( Stream streamOfData, string bankAccountNumber );
    }
}
