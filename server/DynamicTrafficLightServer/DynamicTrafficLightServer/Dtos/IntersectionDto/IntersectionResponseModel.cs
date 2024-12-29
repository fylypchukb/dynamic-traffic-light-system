namespace DynamicTrafficLightServer.Dtos;

public record IntersectionResponseModel
{
    public int Id { get; set; }
    public string City { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public string CreatedByName { get; set; } = string.Empty;
    public DateTime CreateTime { get; set; }
    public string LastUpdatedByName { get; set; } = string.Empty;
    public DateTime LastUpdateTime { get; set; }

    public bool IsActive { get; set; } = true;
}