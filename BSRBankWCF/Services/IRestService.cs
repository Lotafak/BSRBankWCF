using System.IO;
using System.Net.Http;
using System.ServiceModel;
using System.ServiceModel.Web;
using BSRBankWCF.Models;

namespace BSRBankWCF.Services
{
    /// <summary>
    /// Rest service for receiving transfers from another banks
    /// </summary>
    [ServiceContract]
    public interface IRestService
    {
        [OperationContract]
        [WebInvoke(Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            UriTemplate = "{bankAccountNumberTo}")]
        Stream RecieveTransfer( Stream streamOfData, string bankAccountNumberTo );
    }
}
