using System.Reflection;
using SampleApi;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen(options =>
{
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

var app = builder.Build();

app.MapOpenApi();
app.MapSwagger();
app.MapScalarApiReference(options =>
{
    options
        .AddDocument("v1", "Microsoft")
        .AddDocument("v1", "Swashbuckle", "/swagger/{documentName}/swagger.json");
});

app.MapWeatherForecastEndpoints();

app.Run();