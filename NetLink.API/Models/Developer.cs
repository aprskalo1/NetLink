using System.ComponentModel.DataAnnotations;

namespace NetLink.API.Models
{
    public class Developer
    {
        [Key]
        public Guid Id { get; set; }

        public string? DevToken { get; set; } 
        public string? Username { get; set; }

        public ICollection<DeveloperUser>? DeveloperUsers { get; set; }
    }
}
