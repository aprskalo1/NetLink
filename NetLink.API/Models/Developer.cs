using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace NetLink.API.Models
{
    public class Developer
    {
        [Key]
        public Guid Id { get; set; }

        public string? DevToken { get; set; }

        public string? Username { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime? DeletedAt { get; set; }

        public bool Active { get; set; } = true;

        public ICollection<DeveloperUser>? DeveloperUsers { get; set; }
    }
}
