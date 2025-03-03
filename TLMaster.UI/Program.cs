using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using TLMaster.UI;
using TLMaster.UI.Handlers;
using TLMaster.UI.Providers;
using TLMaster.UI.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Services
var apiUrl = builder.Configuration["ApiUrl"] 
    ?? throw new NullReferenceException("Api Url is not defined in configuration.");

builder.Services.AddTransient<AuthenticatedHttpHandler>();
builder.Services.AddTransient<CredentialsHttpHandler>();
builder.Services.AddHttpClient("Credentials", client => client.BaseAddress = new Uri(apiUrl))
    .AddHttpMessageHandler<CredentialsHttpHandler>();
builder.Services.AddHttpClient("Authenticated", client => client.BaseAddress = new Uri(apiUrl))
    .AddHttpMessageHandler<AuthenticatedHttpHandler>();
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<HttpClientProvider>();
builder.Services.AddScoped<TokenProvider>();

await builder.Build().RunAsync();
