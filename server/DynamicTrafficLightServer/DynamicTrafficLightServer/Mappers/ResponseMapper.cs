using DynamicTrafficLightServer.Dtos;

namespace DynamicTrafficLightServer.Mappers;

public static class ResponseMapper
{
    public static ApiResponse<T> ToApiResponse<T>(this ServiceResponse<T> serviceResponse) where T : class?
    {
        return new ApiResponse<T>
        {
            Result = serviceResponse.Result,
            ErrorMessage = serviceResponse.ErrorMessage
        };
    }
}