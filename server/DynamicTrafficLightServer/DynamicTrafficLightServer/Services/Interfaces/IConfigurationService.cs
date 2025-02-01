using DynamicTrafficLightServer.Dtos;

namespace DynamicTrafficLightServer.Services.Interfaces;

public interface IConfigurationService
{
    Task<ServiceResponse<List<ConfigurationResponseModel>>> GetAllAsync(CancellationToken cancellationToken);
    
    Task<ServiceResponse<ConfigurationResponseModel>> GetByIdAsync(int id, CancellationToken cancellationToken);
    
    Task<ServiceResponse<ConfigurationResponseModel>> CreateAsync(ConfigurationRequestModel configurationRequestModel,
        CancellationToken cancellationToken);
    
    Task<ServiceResponse<ConfigurationResponseModel>> UpdateAsync(int id,
        ConfigurationRequestModel configurationRequestModel, CancellationToken cancellationToken);
    
    Task<ServiceResponse<ConfigurationResponseModel>> DeleteAsync(int id, CancellationToken cancellationToken);
}