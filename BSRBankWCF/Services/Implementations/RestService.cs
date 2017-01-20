using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.ServiceModel.Web;
using BSRBankWCF.Converters;
using BSRBankWCF.Models;
using BSRBankWCF.Mongo;
using MongoDB.Driver;
using Newtonsoft.Json;

namespace BSRBankWCF.Services.Implementations
{
    /// <summary>
    /// IRestService interface implementation.
    /// </summary>
    public class RestService : IRestService
    {
        /// <summary>
        /// One and only rest service enpoint hanlding external transfer.
        /// </summary>
        /// <param name="stream">Gets json from request body. Json deserialize to <see cref="Transfer"/></param>
        /// <param name="bankAccountNumberTo">26-digit destination bank account number</param>
        /// <returns></returns>
        public Stream RecieveTransfer( Stream stream, string bankAccountNumberTo )
        {
            var sr = new StreamReader(stream);
            var res = sr.ReadToEnd();

            var ctx = WebOperationContext.Current;
            if ( ctx == null )
                return AccountUtils.CreateJsonErrorResponse("Bład wewnętrzny");

            if ( ctx.IncomingRequest.Headers[HttpRequestHeader.ContentType] != "application/json" )
            {
                ctx.OutgoingResponse.StatusCode = HttpStatusCode.BadRequest;
                return AccountUtils.CreateJsonErrorResponse("Zły nagłówek: " + HttpRequestHeader.ContentType);
            }

            // Using decimal converting when f.e. amount = "0,1"
            var transfer = JsonConvert.DeserializeObject<Transfer>(res, new JsonSerializerSettings { Converters = new List<JsonConverter> { new DecimalConverter() } });

            // Getting WWW-Authenticate header from POST request. eqample "Basic 23sd1"
            // Substring cuts "Basic "
            var credentials = ctx.IncomingRequest.Headers[HttpRequestHeader.Authorization].Substring(6);
            var truth = AccountUtils.Base64Encode("admin:admin");

            // Checks credentials
            if ( truth != credentials )
            {
                ctx.OutgoingResponse.StatusCode = HttpStatusCode.Forbidden;
                return AccountUtils.CreateJsonErrorResponse("Błąd uwierzytelniania");
            }

            // Checks destination bank account number in terms of checksum
            var isValidTo = AccountUtils.ValidateAccountNumber(bankAccountNumberTo);
            if ( !isValidTo )
            {
                ctx.OutgoingResponse.StatusCode = HttpStatusCode.NotFound;
                return AccountUtils.CreateJsonErrorResponse($"Niepoprawne konto: {bankAccountNumberTo}");
            }

            // Checks if destination and source bank account numbers are equal
            if ( bankAccountNumberTo == transfer.From )
            {
                ctx.OutgoingResponse.StatusCode = HttpStatusCode.BadRequest;
                return AccountUtils.CreateJsonErrorResponse("Konto docelowe musi być różne od konta źródłowego !");
            }

            var collection = MongoRepository.GetCollection<User>();
            var filterTo = Builders<User>.Filter.Where(
                x =>
                    x.Accounts.Any(a => a.BankAccountNumber == bankAccountNumberTo));

            var accountTo = collection.Find(filterTo)
                .FirstOrDefault().Accounts.FirstOrDefault(a => a.BankAccountNumber == bankAccountNumberTo);

            // If there is no destination account in database then return 404 code
            if ( accountTo == null )
            {
                ctx.OutgoingResponse.StatusCode = HttpStatusCode.NotFound;
                return AccountUtils.CreateJsonErrorResponse($"Nie znaleziono konta: {bankAccountNumberTo}");
            }

            var decimalAmount = (decimal)transfer.Amount / 100;

            var newAmountTo = accountTo.Amount + decimalAmount;

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
