using System.IO;
using System.Linq;
using System.Net;
using System.ServiceModel.Web;
using BSRBankWCF.Models;
using BSRBankWCF.Mongo;
using MongoDB.Driver;
using Newtonsoft.Json;

namespace BSRBankWCF.Services.Implementations
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "RestService" in both code and config file together.
    public class RestService : IRestService
    {
        public Stream RecieveTransfer( Stream stream, string bankAccountNumber )
        {
            var sr = new StreamReader(stream);
            var res = sr.ReadToEnd();
            var transfer = JsonConvert.DeserializeObject<Transfer>(res);

            var ctx = WebOperationContext.Current;
            if ( ctx == null )
                return AccountUtils.CreateJsonErrorResponse("Coś poszło mocno nie tak");
            
            // Getting WWW-Authenticate header from POST request. eqample "Basic 23sd1"
            // Substring cuts "Basic "
            var credentials = ctx.IncomingRequest.Headers[HttpRequestHeader.Authorization].Substring(6);
            var truth = AccountUtils.Base64Encode("admin:admin");
            if (truth != credentials)
            {
                ctx.OutgoingResponse.StatusCode = HttpStatusCode.Forbidden;
                return AccountUtils.CreateJsonErrorResponse("Błąd uwierzytelniania");
            }

            var isValidTo = AccountUtils.ValidateAccountNumber(bankAccountNumber);

            if ( !isValidTo )
            {
                ctx.OutgoingResponse.StatusCode = HttpStatusCode.NotFound;
                return AccountUtils.CreateJsonErrorResponse($"Niepoprawne konto: {bankAccountNumber}");
            }

            if ( bankAccountNumber == transfer.From )
            {
                ctx.OutgoingResponse.StatusCode = HttpStatusCode.BadRequest;
                return AccountUtils.CreateJsonErrorResponse("Konto docelowe musi być różne od konta źródłowego !");
            }

            var collection = MongoRepository.GetCollection<User>();
            var filterTo = Builders<User>.Filter.Where(
                x =>
                    x.Accounts.Any(a => a.BankAccountNumber == bankAccountNumber));

            var accountTo = collection.Find(filterTo)
                .FirstOrDefault().Accounts.FirstOrDefault(a => a.BankAccountNumber == bankAccountNumber);

            if ( accountTo == null )
            {
                ctx.OutgoingResponse.StatusCode = HttpStatusCode.NotFound;
                return AccountUtils.CreateJsonErrorResponse($"Nie znaleziono konta: {bankAccountNumber}");
            }

            var newAmountTo = accountTo.Amount + transfer.Amount;

            var updateTo = Builders<User>.Update.Set(x => x.Accounts[-1].Amount, newAmountTo);
            var resultTo = collection.UpdateOne(filterTo, updateTo);

            if ( resultTo.IsAcknowledged )
            {
                ctx.OutgoingResponse.StatusCode = HttpStatusCode.Created;
                return null;
            }

            ctx.OutgoingResponse.StatusCode = HttpStatusCode.InternalServerError;
            return AccountUtils.CreateJsonErrorResponse("Bład aktualizacji bazy danych");
        }
    }
}
