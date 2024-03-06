using Microsoft.AspNetCore.Http;

namespace Construct.Rukassa;

public interface IRukassaPaymentSuccessCallbackService
{
    public RukassaPaymentSuccessCallbackRequest GetPaymentCallbackContent(HttpRequest request);
}
