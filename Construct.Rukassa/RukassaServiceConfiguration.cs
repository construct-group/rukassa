using Construct.Rukassa.Implementation;
using Microsoft.Extensions.DependencyInjection;

namespace Construct.Rukassa;

public static class RukassaServiceConfiguration
{
    public static IServiceCollection AddRukassa(this IServiceCollection services, RukassaConfigurationParameters configuration)
    {
        ArgumentNullException.ThrowIfNull(services, nameof(services));
        services.AddSingleton<RukassaConfigurationParameters>(configuration);
        services.AddTransient<IRukassaPaymentCreationService, RukassaPaymentCreationService>();
        
        return services;
    }
}
