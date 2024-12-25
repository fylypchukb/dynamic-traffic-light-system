﻿using DynamicTrafficLightServer.Data;
using DynamicTrafficLightServer.Models;
using DynamicTrafficLightServer.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DynamicTrafficLightServer.Repositories.Implementations;

public class TrafficLightRepository(DataContext context) : ITrafficLightRepository
{
    /// <inheritdoc />
    public async Task<List<TrafficLight>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await context.TrafficLights.AsNoTracking().ToListAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async Task<TrafficLight?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await context.TrafficLights.FindAsync([id], cancellationToken);
    }

    /// <inheritdoc />
    public async Task AddAsync(TrafficLight trafficLight, CancellationToken cancellationToken = default)
    {
        await context.TrafficLights.AddAsync(trafficLight, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async Task UpdateAsync(TrafficLight trafficLight, CancellationToken cancellationToken = default)
    {
        context.TrafficLights.Update(trafficLight);
        await context.SaveChangesAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async Task DeleteAsync(TrafficLight trafficLight, CancellationToken cancellationToken = default)
    {
        context.TrafficLights.Remove(trafficLight);
        await context.SaveChangesAsync(cancellationToken);
    }
}