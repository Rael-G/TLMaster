using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using TLMaster.UI;
using TLMaster.UI.Handlers;
using TLMaster.UI.Mappings;
using TLMaster.UI.Providers;
using TLMaster.UI.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Services
var apiUrl = builder.Configuration["ApiUrl"] 
    ?? throw new NullReferenceException("Api Url is not defined in configuration.");

 builder.Services.AddAutoMapper(typeof(ModelToInput));

builder.Services.AddTransient<AuthenticatedHttpHandler>();
builder.Services.AddTransient<CredentialsHttpHandler>();
builder.Services.AddHttpClient("Credentials", client => client.BaseAddress = new Uri(apiUrl))
    .AddHttpMessageHandler<CredentialsHttpHandler>();
builder.Services.AddHttpClient("Authenticated", client => client.BaseAddress = new Uri(apiUrl))
    .AddHttpMessageHandler<AuthenticatedHttpHandler>();
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<HttpClientProvider>();
builder.Services.AddScoped<TokenProvider>();
builder.Services.AddScoped<UserProvider>();

builder.Services.AddScoped<GuildService>();
builder.Services.AddScoped<CharacterService>();
builder.Services.AddScoped<ItemService>();
builder.Services.AddScoped<ActivityService>();
builder.Services.AddScoped<AuctionService>();
builder.Services.AddScoped<BidService>();
builder.Services.AddScoped<PartyService>();



await builder.Build().RunAsync();
