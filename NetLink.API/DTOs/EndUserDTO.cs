using System.ComponentModel.DataAnnotations;

namespace NetLink.API.DTOs
{
    public class EndUserDTO
    {
        [Required]
        public string? Id { get; set; } // Id returned from external login service
    }
}
