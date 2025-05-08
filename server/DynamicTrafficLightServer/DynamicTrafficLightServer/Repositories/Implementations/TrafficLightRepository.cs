using DynamicTrafficLightServer.Data;
using DynamicTrafficLightServer.Models;
using DynamicTrafficLightServer.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DynamicTrafficLightServer.Repositories.Implementations;

public class TrafficLightRepository(DataContext context) : ITrafficLightRepository
{
    /// <inheritdoc />
    public async Task<List<TrafficLight>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await context.TrafficLights
            .AsNoTracking()
            .Include(i => i.Intersection)
            .Include(i => i.CreatedBy)
            .Include(i => i.LastUpdatedBy)
            .ToListAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async Task<TrafficLight?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await context.TrafficLights
            .Include(i => i.CreatedBy)
            .Include(i => i.LastUpdatedBy)
            .FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
    }

    /// <inheritdoc />
    public async Task AddAsync(TrafficLight trafficLight, CancellationToken cancellationToken)
    {
        await context.TrafficLights.AddAsync(trafficLight, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        await context.Entry(trafficLight)
            .Reference(i => i.CreatedBy)
            .LoadAsync(cancellationToken);

        await context.Entry(trafficLight)
            .Reference(i => i.LastUpdatedBy)
            .LoadAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async Task UpdateAsync(TrafficLight trafficLight, CancellationToken cancellationToken)
    {
        context.TrafficLights.Update(trafficLight);
        await context.SaveChangesAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async Task DeleteAsync(TrafficLight trafficLight, CancellationToken cancellationToken)
    {
        context.TrafficLights.Remove(trafficLight);
        await context.SaveChangesAsync(cancellationToken);
    }
}