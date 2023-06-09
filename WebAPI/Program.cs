using ExternalOidcAuthLibrary.Backend.IoC;
using ExternalOidcAuthLibrary.Backend.MemoryClients.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// external Oidce Auth Library

builder.Services.AddExternalOidcAuthLibraryServices(c =>
builder.Configuration.GetSection(ClientConfigurationOptions.SectionKey).Bind(c),
p => builder.Configuration.GetSection(ExternalOidcAuthLibrary.Backend.MemoryProviders.Options.ProviderConfigurationOptions.SectionKey).
Bind(p));


//builder.Services.Configure<ClientConfigurationOptions>(builder.Configuration.GetSection(ClientConfigurationOptions.SectionKey));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}




app.UseHttpsRedirection();

//var summaries = new[]
//{
//    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
//};

//app.MapGet("/weatherforecast", () =>
//{
//    var forecast = Enumerable.Range(1, 5).Select(index =>
//        new WeatherForecast
//        (
//            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
//            Random.Shared.Next(-20, 55),
//            summaries[Random.Shared.Next(summaries.Length)]


//        ))
//        .ToArray();
//    return forecast;
//})
//.WithName("GetWeatherForecast")
//.WithOpenApi();
app.UseExternalOidcLibraryEndPoints();
app.Run();

