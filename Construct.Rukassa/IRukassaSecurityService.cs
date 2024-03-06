namespace Construct.Rukassa;

public interface IRukassaSecurityService
{
    public bool ValidateSignature(string requestSignature, RukassaPaymentSuccessCallbackRequest request);
}
