namespace BSRBankWCF.Models
{
    /// <summary>
    /// Class representing object sent by REST service, when there's an error in processing request.
    /// </summary>
    class ErrorResponse
    {
        /// <summary>
        /// Error message
        /// </summary>
        public string Error { get; set; }
    }
}
