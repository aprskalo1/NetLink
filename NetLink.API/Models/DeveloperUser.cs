using System.ComponentModel.DataAnnotations;

namespace NetLink.API.Models
{
    public class DeveloperUser
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid DeveloperId { get; set; }

        [Required]
        public string? EndUserId { get; set; }

        public Developer? Developer { get; set; }
        public EndUser? EndUser { get; set; }
    }
}
