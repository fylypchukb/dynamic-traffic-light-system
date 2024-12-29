using System.Net;

namespace DynamicTrafficLightServer.Dtos;

public record ServiceResponse<T> where T : class?
{
    public T? Result { get; init; }
    public string? ErrorMessage { get; set; }
    public HttpStatusCode StatusCode { get; init; } = HttpStatusCode.OK;
    public bool HasError => string.IsNullOrEmpty(ErrorMessage);
}