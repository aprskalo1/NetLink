using System.ComponentModel.DataAnnotations;

namespace NetLink.API.DTOs
{
    public class SensorDTO
    {
        [Required]
        public string? DeviceName { get; set; }

        [Required]
        public string? RecordedValue { get; set; }
    }
}
