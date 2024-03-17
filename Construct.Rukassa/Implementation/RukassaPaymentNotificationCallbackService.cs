using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Construct.Rukassa.Implementation;

internal class RukassaPaymentNotificationCallbackService : IRukassaPaymentNotificationCallbackService
{
    private readonly ILogger<RukassaPaymentSuccessCallbackService> logger;

    public RukassaPaymentNotificationCallbackService(
        ILogger<RukassaPaymentSuccessCallbackService> logger)
    {
        this.logger = logger;
    }

    public RukassaPaymentNotificationCallbackRequest GetPaymentNotificationContent(HttpRequest request)
    {
        logger.LogDebug("{0}: payment notification dispatcher was triggered", DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff"));
        try
        {
            var requestObject = new RukassaPaymentNotificationCallbackRequest
            {
                Id = Convert.ToInt32(request.Form["id"]),
                OrderId = Convert.ToInt32(request.Form["order_id"]),
                Amount = Convert.ToDouble(request.Form["amount"]),
                Way = Convert.ToString(request.Form["way"]),
                Status = RukassaPaymentStatus.FromString(request.Form["status"]!),
            };
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
