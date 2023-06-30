using ExternalOidcAuthLibrary.Blazor.Entities.Options;
using ExternalOidcAuthLibrary.Blazor.IoC;
using ExternalOidcAuthLibrary.Shared.Entities.Interfaces;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using SPAApp;
using SPAApp.FakeServices;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddScoped<IAuthenticationCallback, AuthenticationCallback>();


//register Services
builder.Services.AddBlazorExternalOidcAuthLibraryServices(
    options => builder.Configuration.GetSection($"{AppOptions.SectionKey}").Bind(options));


await builder.Build().RunAsync();
