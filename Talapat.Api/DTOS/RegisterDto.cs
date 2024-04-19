using System.ComponentModel.DataAnnotations;

namespace Talapat.Api.DTOS
{
    public class RegisterDto
    {
        [Required]
        public string DisplayName { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        [Required]
        [RegularExpression(pattern: "^(?=^.{6,10}$)(?=.*\\d)(?=.*[a-z])(?=.*[a-z])(?=.*[!@#$%^&amp;*()_+}{&quot;:;'?/&gt;.&lt;,])(?!.*\\s).*$",ErrorMessage ="Password Must Have One UpperCase, 1 LowerCase , 1 number , ! non alphanumeric and at least 6 characters")]

        public string Password { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Street { get; set; }
    }
}
