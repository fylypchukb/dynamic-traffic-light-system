using DynamicTrafficLightServer.Data;
using DynamicTrafficLightServer.Models;
using DynamicTrafficLightServer.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DynamicTrafficLightServer.Repositories.Implementations;

public class TrafficSwitchLogRepository(DataContext context) : ITrafficSwitchLogRepository
{
    /// <inheritdoc />
    public async Task<List<TrafficSwitchLog>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await context.TrafficSwitchLogs
            .AsNoTracking()
            .Include(l => l.InitBy)
            .ToListAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async Task<TrafficSwitchLog?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await context.TrafficSwitchLogs
            .Include(l => l.InitBy)
            .Include(l => l.TrafficLight)
            .FirstOrDefaultAsync(l => l.Id == id, cancellationToken);
    }

    /// <inheritdoc />
    public async Task AddAsync(TrafficSwitchLog log, CancellationToken cancellationToken = default)
    {
        await context.TrafficSwitchLogs.AddAsync(log, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }
}