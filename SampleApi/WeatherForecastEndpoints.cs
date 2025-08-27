namespace SampleApi;

internal static class WeatherForecastEndpoints
{
    private static readonly string[] Summaries =
    [
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    ];

    internal static void MapWeatherForecastEndpoints(this IEndpointRouteBuilder builder)
    {
        builder
            .MapGet("/weatherforecast", () =>
            {
                var forecast = Enumerable.Range(1, 5).Select(index =>
                    new WeatherForecast
                    (
                        DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                        Summaries[Random.Shared.Next(Summaries.Length)],
                        Random.Shared.Next(-20, 55)
                    ));
                return forecast;
            })
            .WithTags("weather");
    }
}

/// <summary>
/// Represents a weather forecast for a specific date.
/// </summary>
/// <param name="Date">The date of the &lt;b&gt;weather forecast&lt;/b&gt;.</param>
/// <param name="Summary">A brief description of the weather <b>conditions</b>.</param>
/// <param name="TemperatureC">The temperature in <![CDATA[ <b>Celcius</b> ]]>.</param>
public record WeatherForecast(DateOnly Date, string? Summary, int TemperatureC);