using Scalar.AspNetCore;
using TLMaster;
using TLMaster.Application;
using TLMaster.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.ConfigurePersistence(builder.Configuration);
builder.Services.ConfigureApplication();
builder.Services.ConfigureCors(builder.Configuration);
builder.Services.ConfigureDoc();
builder.Services.ConfigureAuth(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger(options =>
    {
        options.RouteTemplate = "/openapi/{documentName}.json";
    });
	app.MapScalarApiReference();

    app.InitializeDb();
}

await app.ConfigureRoles();

app.UseCors();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
