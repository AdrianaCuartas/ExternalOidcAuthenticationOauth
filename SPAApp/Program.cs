using ExternalOidcAuthLibrary.Blazor.Entities.Options;
using ExternalOidcAuthLibrary.Blazor.IoC;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using SPAApp;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

//register Services
builder.Services.AddBlazorExternalOidcAuthLibraryServices(
    options => builder.Configuration.GetSection($"{AppOptions.SectionKey}").Bind(options));


await builder.Build().RunAsync();
