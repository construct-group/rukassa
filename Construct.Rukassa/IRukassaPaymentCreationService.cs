namespace Construct.Rukassa;

public interface IRukassaPaymentCreationService
{
    public Task<RukassaPaymentCreationResponse> CreatePaymentAsync<T>(
        RukassaPaymentCreationRequest<T> request,
        CancellationToken cancellationToken = default)
            where T : RukassaRequestData;
}
