using Construct.Rukassa.Implementation;
using Microsoft.Extensions.DependencyInjection;

namespace Construct.Rukassa;

public static class RukassaServiceConfiguration
{
    public static IServiceCollection AddRukassa(this IServiceCollection services, RukassaConfigurationParameters configuration)
    {
        ArgumentNullException.ThrowIfNull(services, nameof(services));
        services.AddSingleton(configuration);
        services.AddTransient<IRukassaPaymentCreationService, RukassaPaymentCreationService>();
        services.AddTransient<IRukassaPaymentInfoService, RukassaPaymentInfoService>();
        services.AddTransient<IRukassaSecurityService, RukassaSecurityService>();
        services.AddTransient<IRukassaPaymentSuccessCallbackService, RukassaPaymentSuccessCallbackService>();
        return services;
    }
}
