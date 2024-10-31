namespace NetLink.API.DTOs.Response;

public class PagedEndUserResponseDto
{
    public List<EndUserResponseDto> EndUsers { get; set; }
    public int TotalCount { get; set; }
}