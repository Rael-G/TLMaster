using TLMaster.Api.Mappings;

namespace TLMaster.Api;

public static class ApiExtensions
{
    /// <summary>
    /// Configures api-related services.
    /// </summary>
    /// <param name="services">The collection of services to configure.</param>
    public static void ConfigureApplication(this IServiceCollection services)
    {
        // Registers the AutoMapper service
        services.AddAutoMapper(typeof(InputToDto));
    }
}
