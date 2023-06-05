


//[x]As a user of the API, I want to be able to get current weather data (temperature, humidity, wind) for Stockholm.
//[]Som anv�ndare av API:et vill jag kunna spara en favoritstad och slippa ange den varje g�ng(Obs att det bara ska sparas s� l�nge appen k�rs, allts� inte mellan k�rningar)
//[]Som system�gare vill jag kunna se om API:et k�rs(health check)
//[]Som system�gare vill jag kunna se statistik p� antal anrop sen API:et startades
//[]Som slutanv�ndare av Reactklienten vill jag kunna se aktuellt v�der f�r Stockholm
//[]Som slutanv�ndare av Reactklienten vill jag kunna se och spara favoritstad
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/weatherforecast-Stockholm", () =>
{
    var forecast = new WeatherForecast
    (
        DateTime.Now,
        new WeatherData
        (
            "Stockholm",
            new WeatherProperty("Temperature", 23, "�C"),
            new WeatherProperty("Humidity", 65, "%"),
            new WeatherProperty("Wind", 12.5, "km/h")
        )
    );

    return forecast;
})
.WithName("GetCurrentWeatherData");


app.MapGet("/API-health", () =>
{
    return ($"API is healthy {StatusCodes.Status200OK}");
});



app.Run();

public record WeatherForecast(DateTime Date, WeatherData WeatherData);

public record WeatherData(string Location, WeatherProperty Temperature, WeatherProperty Humidity, WeatherProperty Wind);

public record WeatherProperty(string Name, double Value, string Unit);
