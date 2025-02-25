using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using TLMaster.UI.Components;
using TLMaster.UI.Providers;
using TLMaster.UI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Services
var apiUrl = builder.Configuration["ApiUrl"] 
    ?? throw new NullReferenceException("Api Url is not defined in configuration.");
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(apiUrl) });
builder.Services.AddScoped<ApplicationAuthStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider>(provider => 
        provider.GetRequiredService<ApplicationAuthStateProvider>());
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<HttpClientProvider>();
builder.Services.AddBlazoredLocalStorage();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
