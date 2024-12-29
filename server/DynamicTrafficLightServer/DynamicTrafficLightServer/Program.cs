using System.Reflection;
using DynamicTrafficLightServer.Data;
using DynamicTrafficLightServer.Repositories.Implementations;
using DynamicTrafficLightServer.Repositories.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DataContext>(opt =>
    opt.UseSqlServer(new SqlConnection(builder.Configuration.GetConnectionString("DefaultConnection"))));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt =>
{
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    opt.IncludeXmlComments(xmlPath, true);
});

builder.Services.AddScoped<IIntersectionRepository, IntersectionRepository>();
builder.Services.AddScoped<ITrafficLightRepository, TrafficLightRepository>();
builder.Services.AddScoped<IConfigurationRepository, ConfigurationRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(options =>
    {
        options.RouteTemplate = "/openapi/{documentName}.json";
    });
    
    app.MapScalarApiReference(opt =>
    {
        opt.HideClientButton = true;
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();