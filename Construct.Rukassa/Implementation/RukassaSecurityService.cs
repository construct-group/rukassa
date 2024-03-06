using System.Security.Cryptography;
using System.Text;

namespace Construct.Rukassa.Implementation;

internal class RukassaSecurityService : IRukassaSecurityService
{
    private readonly RukassaConfigurationParameters rukassaServiceConfiguration;
    
    public RukassaSecurityService(RukassaConfigurationParameters rukassaConfigurationParameters)
    {
        this.rukassaServiceConfiguration = rukassaConfigurationParameters;
    }

    public bool ValidateSignature(string requestSignature, RukassaPaymentSuccessCallbackRequest request)
    {
        var hash = new HMACSHA256(Encoding.UTF8.GetBytes(rukassaServiceConfiguration.Token));
        var requestData = $"{request.Id}|{request.CreatedDateTime}|{request.Amount}";
        var signature = hash.ComputeHash(Encoding.UTF8.GetBytes(requestData));
        return Convert.ToBase64String(signature).Equals(requestSignature);
    }
}
