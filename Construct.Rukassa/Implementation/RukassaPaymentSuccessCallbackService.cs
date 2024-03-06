using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net;

namespace Construct.Rukassa.Implementation;

internal class RukassaPaymentSuccessCallbackService : IRukassaPaymentSuccessCallbackService
{
    private readonly IRukassaSecurityService rukassaSecurityService;
    private readonly ILogger<RukassaPaymentSuccessCallbackService> logger;

    public RukassaPaymentSuccessCallbackService(
        IRukassaSecurityService rukassaSecurityService,
        ILogger<RukassaPaymentSuccessCallbackService> logger)
    {
        this.rukassaSecurityService = rukassaSecurityService;
        this.logger = logger;
    }

    public RukassaPaymentSuccessCallbackRequest GetPaymentCallbackContent(HttpRequest request)
    {
        logger.LogDebug("{0}: payment callback dispatcher was triggered", DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff"));
        try
        {
            var requestObject = new RukassaPaymentSuccessCallbackRequest
            {
                Id = Convert.ToInt32(request.Form["id"]),
                OrderId = Convert.ToInt32(request.Form["order_id"]),
                Amount = (float)Convert.ToDouble(request.Form["amount"]),
                InAmount = (float)Convert.ToDouble(request.Form["in_amount"]),
                Data = JsonConvert.DeserializeObject<RukassaRequestData>(WebUtility.HtmlDecode(request.Form["data"]!))!,
                CreatedDateTime = DateTime.Parse(request.Form["createdDateTime"]!),
                Status = RukassaPaymentStatus.FromString(request.Form["status"]!),
            };

            if (request.Headers.TryGetValue("HTTP_SIGNATURE", out var signature) == false) throw new ArgumentNullException("Signature was not found");
            if (rukassaSecurityService.ValidateSignature(signature!, requestObject) == false) throw new ArgumentException("ERROR SIGN");
            if (requestObject.InAmount < requestObject.Amount) throw new ArgumentException("ERROR AMOUNT");
            return requestObject;
        }
        catch (ArgumentException ex)
        {
            logger.LogWarning(ex, "{0}: payment callback dispatcher issued with a warning ({1})", DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff"), ex.Message);
            throw;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "{0}: payment callback dispatcher issued with an error ({1})", DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff"), ex.Message);
            throw;
        }
    }
}
