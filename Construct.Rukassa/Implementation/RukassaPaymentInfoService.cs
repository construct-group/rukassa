using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net;

namespace Construct.Rukassa.Implementation;

internal class RukassaGetPaymentInfoResponseJson
{
    [JsonProperty("id")] public int Id { get; set; }
    [JsonProperty("order_id")] public int OrderId { get; set; }
    [JsonProperty("amount")] public double Amount { get; set; }
    [JsonProperty("in_amount")] public double InAmount { get; set; }
    [JsonProperty("currency")] public string Currency { get; set; } = null!;
    [JsonProperty("method")] public string Method { get; set; } = null!;
    [JsonProperty("status")] public string Status { get; set; } = null!;
    [JsonProperty("data")] public RukassaRequestData Data { get; set; } = null!;
}

internal class RukassaGetPaymentWithdrawResponseJson
{
    [JsonProperty("id")] public int Id { get; set; }
    [JsonProperty("order_id")] public int OrderId { get; set; }
    [JsonProperty("amount")] public double Amount { get; set; }
    [JsonProperty("fee")] public double Fee { get; set; }
    [JsonProperty("way")] public string Way { get; set; } = null!;
    [JsonProperty("who_fee")] public string WhoFee { get; set; } = null!;
    [JsonProperty("status")] public string Status { get; set; } = null!;
}

internal class RukassaPaymentInfoService : IRukassaPaymentInfoService
{
    private readonly RukassaConfigurationParameters rukassaConfigurationParameters;
    private readonly ILogger<RukassaPaymentInfoService> logger;

    public RukassaPaymentInfoService(
        RukassaConfigurationParameters rukassaConfigurationParameters, 
        ILogger<RukassaPaymentInfoService> logger)
    {
        this.rukassaConfigurationParameters = rukassaConfigurationParameters;
        this.logger = logger;
    }

    public async Task<RukassaGetPaymentInfoResponse> GetPaymentInfoAsync(
        RukassaGetPaymentInfoRequest request,
        CancellationToken cancellationToken = default)
    {
        logger.LogDebug("{0} {1}: payment info requested", DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff"), request.Id);
        using var client = new HttpClient();
        var content = new FormUrlEncodedContent(new List<KeyValuePair<string, string?>>()
        {
            new("id", request.Id.ToString()),
            new("shop_id", request.ShopId.ToString()),
            new("token", rukassaConfigurationParameters.Token)
        });
        var response = await client.PostAsync($"{rukassaConfigurationParameters.BaseUrl}/getPayInfo", content, cancellationToken);
        var stringResponseContent = await response.Content.ReadAsStringAsync();
        if (response.IsSuccessStatusCode == false)
        {
            var result = JsonConvert.DeserializeObject<RukassaErrorResponse>(stringResponseContent);
            if (result is null)
            {
                logger.LogCritical("{0} {1}: payment info error response deserialisation unexpectedly failed",
                    DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff"), request.Id);
                throw new ArgumentNullException("Payment info error response deserialisation unexpectedly failed");
            }
            logger.LogDebug("{0} {1}: payment info request failed with code {2} ({3})",
                DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff"), request.Id, result.Code, result.Message);
            throw new HttpRequestException(result!.Message, null, (HttpStatusCode)result.Code);
        }
        else
        {
            var result = JsonConvert.DeserializeObject<RukassaGetPaymentInfoResponseJson>(stringResponseContent);
            if (result is null)
            {
                logger.LogCritical("{0} {1}: payment info response deserialisation unexpectedly failed",
                    DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff"), request.Id);
                throw new ArgumentNullException("Payment info response deserialisation unexpectedly failed");
            }
            return new()
            {
                Id = result.Id,
                OrderId = result.OrderId,
                Amount = result.Amount,
                InAmount = result.InAmount,
                Currency = RukassaPaymentCurrency.FromString(result.Currency),
                Method = RukassaPaymentMethod.FromString(result.Method),
                Status = RukassaPaymentStatus.FromString(result.Status),
                Data = result.Data,
            };
        }
    }

    public async Task<RukassaGetWithdrawInfoResponse> GetWithdrawInfoAsync(
        RukassaGetWithdrawInfoRequest request,
        CancellationToken cancellationToken = default)
    {
        logger.LogDebug("{0} {1}: payment withdraw info requested", DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff"), request.Id);
        using var client = new HttpClient();
        var content = new FormUrlEncodedContent(new List<KeyValuePair<string, string?>>()
        {
            new("id", request.Id.ToString()),
            new("shop_id", request.ShopId.ToString()),
            new("token", rukassaConfigurationParameters.Token)
        });
        var response = await client.PostAsync($"{rukassaConfigurationParameters.BaseUrl}/getWithdrawInfo", content, cancellationToken);
        var stringResponseContent = await response.Content.ReadAsStringAsync();
        if (response.IsSuccessStatusCode == false)
        {
            var result = JsonConvert.DeserializeObject<RukassaErrorResponse>(stringResponseContent);
            if (result is null)
            {
                logger.LogCritical("{0} {1}: payment withdraw info error response deserialisation unexpectedly failed",
                    DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff"), request.Id);
                throw new ArgumentNullException("Payment withdraw info error response deserialisation unexpectedly failed");
            }
            logger.LogDebug("{0} {1}: payment withdraw info request failed with code {2} ({3})",
                DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff"), request.Id, result.Code, result.Message);
            throw new HttpRequestException(result!.Message, null, (HttpStatusCode)result.Code);
        }
        else
        {
            var result = JsonConvert.DeserializeObject<RukassaGetPaymentWithdrawResponseJson>(stringResponseContent);
            if (result is null)
            {
                logger.LogCritical("{0} {1}: payment withdraw info response deserialisation unexpectedly failed",
                    DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff"), request.Id);
                throw new ArgumentNullException("Payment withdraw info response deserialisation unexpectedly failed");
            }
            return new()
            {
                Id = result.Id,
                OrderId = result.OrderId,
                Amount = result.Amount,
                Fee = result.Fee,
                Way = result.Way,
                WhoFee = RukassaPaymentWhoFee.FromString(result.WhoFee),
                Status = RukassaPaymentStatus.FromString(result.Status),
            };
        }
    }
}
