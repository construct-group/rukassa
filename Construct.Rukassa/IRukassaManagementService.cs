namespace Construct.Rukassa;

public interface IRukassaManagementService
{
    public Task<RukassaGetBalanceResponse> GetBalanceAsync(CancellationToken cancellationToken = default);
    public Task<RukassaCreateWithdrawResponse> CreateWithdrawAsync(RukassaCreateWithdrawRequest request, CancellationToken cancellationToken = default);
    public Task<RukassaCancelWithdrawResponse> CancelWithdrawAsync(int id, CancellationToken cancellationToken = default);
}
