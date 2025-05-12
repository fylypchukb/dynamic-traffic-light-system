using DynamicTrafficLightServer.Data;
using DynamicTrafficLightServer.Models;
using DynamicTrafficLightServer.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DynamicTrafficLightServer.Repositories.Implementations;

public class ConfigurationRepository(DataContext context) : IConfigurationRepository
{
    /// <inheritdoc />
    public async Task<List<Configuration>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await context.Configurations
            .AsNoTracking()
            .Include(i => i.CreatedBy)
            .Include(i => i.LastUpdatedBy)
            .Include(i => i.TrafficLight)
            .ToListAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async Task<Configuration?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await context.Configurations
            .Include(i => i.CreatedBy)
            .Include(i => i.LastUpdatedBy)
            .FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
    }

    /// <inheritdoc />
    public async Task AddAsync(Configuration configurations, CancellationToken cancellationToken)
    {
        await context.Configurations.AddAsync(configurations, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        await context.Entry(configurations)
            .Reference(i => i.CreatedBy)
            .LoadAsync(cancellationToken);

        await context.Entry(configurations)
            .Reference(i => i.LastUpdatedBy)
            .LoadAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async Task UpdateAsync(Configuration configurations, CancellationToken cancellationToken)
    {
        context.Configurations.Update(configurations);
        await context.SaveChangesAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async Task DeleteAsync(Configuration configurations, CancellationToken cancellationToken)
    {
        context.Configurations.Remove(configurations);
        await context.SaveChangesAsync(cancellationToken);
    }

    /// <inheritdoc />
    public Task<Configuration?> GetByTrafficLightIdAsync(int trafficLightId, CancellationToken cancellationToken)
    {
        return context.Configurations
            .AsNoTracking()
            .Include(c => c.TrafficLight)
            .FirstOrDefaultAsync(s => s.TrafficLightId == trafficLightId && s.IsActive, cancellationToken);
    }
}