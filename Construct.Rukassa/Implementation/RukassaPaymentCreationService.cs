using Microsoft.Extensions.Logging;

namespace Construct.Rukassa.Implementation;

internal class RukassaPaymentCreationService : IRukassaPaymentCreationService
{
    private readonly RukassaConfigurationParameters rukassaConfigurationParameters;
    private readonly ILogger<RukassaPaymentCreationService> logger;

    public RukassaPaymentCreationService(
        RukassaConfigurationParameters rukassaConfigurationParameters,
        ILogger<RukassaPaymentCreationService> logger)
    {
        this.rukassaConfigurationParameters = rukassaConfigurationParameters;
        this.logger = logger;
    }

    public async Task<RukassaPaymentCreationResponse> CreatePaymentAsync<T>(
        RukassaPaymentCreationRequest<T> request, 
        CancellationToken cancellationToken = default) 
            where T : RukassaRequestData
    {
        logger.LogDebug("{0}: payment creation requested - id{1}", DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff"), request.OrderId);
        using var client = new HttpClient();
        var content = new FormUrlEncodedContent(new List<KeyValuePair<string, string>>()
        {
            new("shop_id", rukassaConfigurationParameters.ShopId.ToString()),
            new("token", rukassaConfigurationParameters.Token.ToString()),
            new("order_id", request.OrderId.ToString()),
            new("amount", request.Amount.ToString()),
        });
    }
}
