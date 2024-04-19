using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Talapat.DAL.Entities.IdentityEntities
{
    public class Address
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        [Required]
        [ForeignKey("User")]
        public string UserId { get; set; }
        public AppUser User { get; set; }
    }
}