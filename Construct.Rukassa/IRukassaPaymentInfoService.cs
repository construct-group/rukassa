namespace Construct.Rukassa;

public interface IRukassaPaymentInfoService
{
    public Task<RukassaGetPaymentInfoResponse> GetPaymentInfoAsync(
        RukassaGetPaymentInfoRequest request,
        CancellationToken cancellationToken = default);
}
