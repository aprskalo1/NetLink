using System.ComponentModel.DataAnnotations;

namespace NetLink.API.DTOs
{
    public class RecordedValueDTO
    {
        [Required] 
        public string? Value { get; set; }
    }
}
