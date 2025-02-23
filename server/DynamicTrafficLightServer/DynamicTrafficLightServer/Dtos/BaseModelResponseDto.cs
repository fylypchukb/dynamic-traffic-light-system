namespace DynamicTrafficLightServer.Dtos;

public abstract record BaseModelResponseDto
{
    /// <summary>
    /// The unique identifier.
    /// </summary>
    public int Id { get; set; }
}