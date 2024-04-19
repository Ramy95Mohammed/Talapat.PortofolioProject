
namespace Talapat.Api.Errors
{
    public class ApiResponse
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public ApiResponse(int statusCode,string message = null)
        {
            this.StatusCode = statusCode;
            this.Message = message?? GetDefaultMessageForStatusCode(statusCode);
        }

        private string? GetDefaultMessageForStatusCode(int statusCode)
        => statusCode switch
        {
            400 => "A Bad Request , You Have Made",
            401 => "Authorized, You Are Not",
            404 => "Resource Not Found",
            500 => "Errors Are The Path To dark side . Errors lead to anger ,anger lead to hate . Hate leads to career scape",
            _=> null
        };
            
         
    }
}
