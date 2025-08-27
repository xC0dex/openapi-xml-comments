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
builder.Services.AddOpenApiDocument();

var app = builder.Build();

app.MapOpenApi();
app.MapSwagger("/swashbuckle/{documentName}.json");
app.UseOpenApi(options => options.Path = "/nswag/{documentName}.json");

app.MapScalarApiReference(options =>
{
    options
        .AddDocument("v1", "Microsoft")
        .AddDocument("v1", "Swashbuckle", "/swashbuckle/{documentName}.json")
        .AddDocument("v1", "NSwag", "/nswag/{documentName}.json");
});

app.MapWeatherForecastEndpoints();

app.Run();