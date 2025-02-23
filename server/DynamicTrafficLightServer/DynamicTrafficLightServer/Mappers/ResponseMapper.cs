using System.Net;
using DynamicTrafficLightServer.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace DynamicTrafficLightServer.Mappers;

public static class ResponseMapper
{
    public static ObjectResult ToApiResponse<T>(this ServiceResponse<T> serviceResponse) where T : class?
    {
        return new ObjectResult(new ApiResponse<T>
        {
            Result = serviceResponse.Result,
            ErrorMessage = serviceResponse.ErrorMessage
        })
        {
            StatusCode = (int)serviceResponse.StatusCode
        };
    }

    public static ObjectResult ToCreatedApiResponse<T>(this ServiceResponse<T> serviceResponse, string route)
        where T : class?
    {
        if (serviceResponse is not { StatusCode: HttpStatusCode.Created, Result: BaseModelResponseDto baseResponseDto })
        {
            return new ObjectResult(new ApiResponse<T>
            {
                Result = serviceResponse.Result,
                ErrorMessage = serviceResponse.ErrorMessage
            })
            {
                StatusCode = (int)serviceResponse.StatusCode
            };
        }

        var location = $"{route}/{baseResponseDto.Id}";

        return new CreatedResult(location, new ApiResponse<T>
        {
            Result = serviceResponse.Result,
            ErrorMessage = serviceResponse.ErrorMessage
        });
    }
}