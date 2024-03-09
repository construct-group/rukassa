using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net;

namespace Construct.Rukassa.Implementation;

internal class RukassaCreateWithdrawResponseJson
{
    [JsonProperty("id")] public int Id { get; set; }
    [JsonProperty("status")] public string Status { get; set; } = null!;
}

internal class RukassaManagementService : IRukassaManagementService
{
    private readonly RukassaConfigurationParameters rukassaConfigurationParameters;
    private readonly ILogger<RukassaPaymentCreationService> logger;

    public RukassaManagementService(
        RukassaConfigurationParameters rukassaConfigurationParameters,
        ILogger<RukassaPaymentCreationService> logger)
    {
        this.rukassaConfigurationParameters = rukassaConfigurationParameters;
        this.logger = logger;
    }

    public async Task<RukassaGetBalanceResponse> GetBalanceAsync(
        CancellationToken cancellationToken = default)
    {
        logger.LogDebug("{0}: get balance requested", DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff"));
        ArgumentNullException.ThrowIfNull(rukassaConfigurationParameters.Email);
        using var client = new HttpClient();
        var content = new FormUrlEncodedContent(new List<KeyValuePair<string, string?>>()
        {
            new("email", rukassaConfigurationParameters.Email),
            new("password", rukassaConfigurationParameters.Password)
        });
        var response = await client.PostAsync($"{rukassaConfigurationParameters.BaseUrl}/getBalance", content, cancellationToken);
        var stringResponseContent = await response.Content.ReadAsStringAsync(cancellationToken);
        if (response.IsSuccessStatusCode == false)
        {
            var result = JsonConvert.DeserializeObject<RukassaErrorResponse>(stringResponseContent);
            if (result is null)
            {
                logger.LogCritical("{0}: get balance error response deserialisation unexpectedly failed",
                    DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff"));
                throw new ArgumentNullException("Get balance error response deserialisation unexpectedly failed");
            }
            logger.LogDebug("{0}: get balance request failed with code {2} ({3})",
                DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff"), result.Code, result.Message);
            throw new HttpRequestException(result!.Message, null, (HttpStatusCode)result.Code);
        }
        else
        {
            var result = JsonConvert.DeserializeObject<RukassaGetBalanceResponse>(stringResponseContent);
            if (result is null)
            {
                logger.LogCritical("{0}: get balance response deserialisation unexpectedly failed",
                    DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff"));
                throw new ArgumentNullException("Get balance response deserialisation unexpectedly failed");
            }
            return result;
        }
    }

    public async Task<RukassaCreateWithdrawResponse> CreateWithdrawAsync(
        RukassaCreateWithdrawRequest request, 
        CancellationToken cancellationToken = default)
    {
        logger.LogDebug("{0}: create withdraw requested", DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff"));
        ArgumentNullException.ThrowIfNull(rukassaConfigurationParameters.Email);
        using var client = new HttpClient();
        var content = new FormUrlEncodedContent(new List<KeyValuePair<string, string?>>()
        {
            new("email", rukassaConfigurationParameters.Email),
            new("password", rukassaConfigurationParameters.Password),
            new("way", request.Way.ToString()),
            new("wallet", request.Wallet),
            new("amount", request.Amount.ToString()),
            new("from", request.From?.ToString()),
            new("order_id", request.OrderId?.ToString()),
            new("who_fee", request.WhoFee?.Id.ToString()),
            new("bank", request.Bank?.ToString()),
        }.Where(x => x.Value is not null));
        var response = await client.PostAsync($"{rukassaConfigurationParameters.BaseUrl}/createWithdraw", content, cancellationToken);
        var stringResponseContent = await response.Content.ReadAsStringAsync(cancellationToken);
        if (response.IsSuccessStatusCode == false)
        {
            var result = JsonConvert.DeserializeObject<RukassaErrorResponse>(stringResponseContent);
            if (result is null)
            {
                logger.LogCritical("{0}: create withdraw error response deserialisation unexpectedly failed",
                    DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff"));
                throw new ArgumentNullException("Create withdraw error response deserialisation unexpectedly failed");
            }
            logger.LogDebug("{0}: create withdraw request failed with code {2} ({3})",
                DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff"), result.Code, result.Message);
            throw new HttpRequestException(result!.Message, null, (HttpStatusCode)result.Code);
        }
        else
        {
            var result = JsonConvert.DeserializeObject<RukassaCreateWithdrawResponseJson>(stringResponseContent);
            if (result is null)
            {
                logger.LogCritical("{0}: create withdraw response deserialisation unexpectedly failed",
                    DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff"));
                throw new ArgumentNullException("Create withdraw response deserialisation unexpectedly failed");
            }
            return new()
            {
                Id = result.Id,
                Status = RukassaPaymentStatus.FromString(result.Status),
            };
        }
    }

    public async Task<RukassaCancelWithdrawResponse> CancelWithdrawAsync(int id, CancellationToken cancellationToken = default)
    {
        logger.LogDebug("{0}: cancel withdraw requested", DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff"));
        ArgumentNullException.ThrowIfNull(rukassaConfigurationParameters.Email);
        using var client = new HttpClient();
        var content = new FormUrlEncodedContent(new List<KeyValuePair<string, string?>>()
        {
            new("email", rukassaConfigurationParameters.Email),
            new("password", rukassaConfigurationParameters.Password),
            new("id", id.ToString()),
        });
        var response = await client.PostAsync($"{rukassaConfigurationParameters.BaseUrl}/cancelWithdraw", content, cancellationToken);
        var stringResponseContent = await response.Content.ReadAsStringAsync(cancellationToken);
        if (response.IsSuccessStatusCode == false)
        {
            var result = JsonConvert.DeserializeObject<RukassaErrorResponse>(stringResponseContent);
            if (result is null)
            {
                logger.LogCritical("{0}: cancel withdraw error response deserialisation unexpectedly failed",
                    DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff"));
                throw new ArgumentNullException("Cancel withdraw error response deserialisation unexpectedly failed");
            }
            logger.LogDebug("{0}: cancel withdraw request failed with code {2} ({3})",
                DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff"), result.Code, result.Message);
            throw new HttpRequestException(result!.Message, null, (HttpStatusCode)result.Code);
        }
        else
        {
            var result = JsonConvert.DeserializeObject<RukassaCreateWithdrawResponseJson>(stringResponseContent);
            if (result is null)
            {
                logger.LogCritical("{0}: cancel withdraw response deserialisation unexpectedly failed",
                    DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff"));
                throw new ArgumentNullException("Cancel withdraw response deserialisation unexpectedly failed");
            }
            return new()
            {
                Id = result.Id,
                Status = RukassaPaymentStatus.FromString(result.Status),
            };
        }
    }
}
