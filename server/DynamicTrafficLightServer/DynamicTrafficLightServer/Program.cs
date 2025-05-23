using System.Reflection;
using System.Text.Json.Serialization;
using DynamicTrafficLightServer;
using DynamicTrafficLightServer.Data;
using DynamicTrafficLightServer.Repositories.Implementations;
using DynamicTrafficLightServer.Repositories.Interfaces;
using DynamicTrafficLightServer.Services.Implementations;
using DynamicTrafficLightServer.Services.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocal", policy =>
    {
        policy.WithOrigins("http://localhost:3000")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

builder.Services.AddDbContext<DataContext>(opt =>
    opt.UseSqlServer(new SqlConnection(builder.Configuration.GetConnectionString("DefaultConnection"))));

builder.Services.AddControllers()
    .AddJsonOptions(opt => { opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()); });

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSignalR();

builder.Services.AddSwaggerGen(opt =>
{
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    opt.IncludeXmlComments(xmlPath, true);
    opt.UseInlineDefinitionsForEnums();
});

builder.Services.AddScoped<IIntersectionRepository, IntersectionRepository>();
builder.Services.AddScoped<ITrafficLightRepository, TrafficLightRepository>();
builder.Services.AddScoped<IConfigurationRepository, ConfigurationRepository>();

builder.Services.AddScoped<IIntersectionService, IntersectionService>();
builder.Services.AddScoped<ITrafficLightService, TrafficLightService>();
builder.Services.AddScoped<IConfigurationService, ConfigurationService>();

builder.Services.AddScoped<ITrafficFlowService, TrafficFlowService>();

builder.Services.AddScoped<IEntityChangeLogRepository, EntityChangeLogRepository>();
builder.Services.AddScoped<ITrafficSwitchLogRepository, TrafficSwitchLogRepository>();

builder.Services.AddScoped<ILogsService, LogsService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger(options => { options.RouteTemplate = "/openapi/{documentName}.json"; });

    app.MapScalarApiReference(opt => { opt.HideClientButton = true; });
}

app.UseCors("AllowLocal");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.MapHub<TrafficHub>("/trafficHub");

app.Run();