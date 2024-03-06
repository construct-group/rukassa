using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net;

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
        logger.LogDebug("{0} {1}: payment creation requested", DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff"), request.OrderId);
        using var client = new HttpClient();
        var content = new FormUrlEncodedContent(new List<KeyValuePair<string, string?>>()
        {
            new("shop_id", rukassaConfigurationParameters.ShopId.ToString()),
            new("token", rukassaConfigurationParameters.Token.ToString()),
            new("order_id", request.OrderId.ToString()),
            new("amount", request.Amount.ToString()),
            new("data", request.Data is null ? null : JsonConvert.SerializeObject(request.Data)),
            new("method", request.Method?.Value),
            new("currency", request.Currency?.Value),
            new("user_code", request.UserCode),
            new("json", request.Json?.ToString())
        }.Where(kv => kv.Value is not null));
        var response = await client.PostAsync($"{rukassaConfigurationParameters.BaseUrl}/create", content, cancellationToken);
        var stringResponseContent = await response.Content.ReadAsStringAsync();
        if (response.IsSuccessStatusCode == false)
        {
            var result = JsonConvert.DeserializeObject<RukassaErrorResponse>(stringResponseContent);
            if (result is null)
            {
                logger.LogCritical("{0} {1}: payment creation error response deserialisation unexpectedly failed", 
                    DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff"), request.OrderId);
                throw new ArgumentNullException("Payment creation error response deserialisation unexpectedly failed");
            }
            logger.LogDebug("{0} {1}: payment creation request failed with code {2} ({3})", 
                DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff"), request.OrderId, result.Code, result.Message);
            throw new HttpRequestException(result!.Message, null, (HttpStatusCode)result.Code);
        }
        else
        {
            var result = JsonConvert.DeserializeObject<RukassaPaymentCreationResponse>(stringResponseContent);
            if (result is null)
            {
                logger.LogCritical("{0} {1}: payment creation response deserialisation unexpectedly failed",
                    DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff"), request.OrderId);
                throw new ArgumentNullException("Payment creation response deserialisation unexpectedly failed");
            }
            return result;
        }
    }
}
