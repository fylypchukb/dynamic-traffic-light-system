﻿using DynamicTrafficLightServer.Data;
using DynamicTrafficLightServer.Models;
using DynamicTrafficLightServer.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DynamicTrafficLightServer.Repositories.Implementations;

public class ConfigurationRepository(DataContext context) : IConfigurationRepository
{
    /// <inheritdoc />
    public async Task<List<Configuration>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await context.Configurations.AsNoTracking().ToListAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async Task<Configuration?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await context.Configurations.FindAsync([id], cancellationToken);
    }

    /// <inheritdoc />
    public async Task AddAsync(Configuration configurations, CancellationToken cancellationToken = default)
    {
        await context.Configurations.AddAsync(configurations, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async Task UpdateAsync(Configuration configurations, CancellationToken cancellationToken = default)
    {
        context.Configurations.Update(configurations);
        await context.SaveChangesAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async Task DeleteAsync(Configuration configurations, CancellationToken cancellationToken = default)
    {
        context.Configurations.Remove(configurations);
        await context.SaveChangesAsync(cancellationToken);
    }
}