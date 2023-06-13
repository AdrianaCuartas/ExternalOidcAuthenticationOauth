using ExternalOidcAuthLibrary.Backend.IoC;
using ExternalOidcAuthLibrary.Backend.MemoryClients.Options;
using ExternalOidcAuthLibrary.Backend.MemoryProviders.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// external Oidce Auth Library

builder.Services.AddExternalOidcAuthLibraryServices(
    c =>
      builder.Configuration.GetSection(
       ClientConfigurationOptions.SectionKey).Bind(c),
    p =>
      builder.Configuration.GetSection
      (ProviderConfigurationOptions.SectionKey).Bind(p));


//builder.Services.Configure<ClientConfigurationOptions>(builder.Configuration.GetSection(ClientConfigurationOptions.SectionKey));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseExternalOidcLibraryEndPoints();
app.Run();

