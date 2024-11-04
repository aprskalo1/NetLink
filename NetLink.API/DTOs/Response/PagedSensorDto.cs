namespace NetLink.API.DTOs.Response;

public class PagedSensorDto
{
    public List<SensorResponseDto> Sensors { get; set; }
    public int TotalCount { get; set; }
}