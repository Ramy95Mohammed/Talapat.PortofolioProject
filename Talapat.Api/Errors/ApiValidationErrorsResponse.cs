namespace Talapat.Api.Errors
{
    public class ApiValidationErrorsResponse : ApiResponse
    {
        public ApiValidationErrorsResponse():base(400)
        {
                
        }
        public IEnumerable<string> Errors { get; set; }
    }
}
