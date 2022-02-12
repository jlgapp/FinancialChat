namespace FinancialChat.Api.Errors
{
    public class CodeErrorResponse
    {
        public int StatusCode { get; set; }
        public string? Message { get; set; }
        public CodeErrorResponse(int statusCode, string? message = null)
        {
            this.StatusCode = statusCode;
            this.Message = message ?? GetDefaultMessageStatusCode(statusCode);
        }

        private string GetDefaultMessageStatusCode(int statusCode)
        {
            return statusCode switch
            {
                400 => "El request has errores",
                401 => "Unahthorized",
                404 => "Not Fpund",
                500 => "Server has errores",
                _ => String.Empty
            };
        }
    }
}
