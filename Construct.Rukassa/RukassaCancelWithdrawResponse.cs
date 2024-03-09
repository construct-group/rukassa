namespace Construct.Rukassa;

public record RukassaCancelWithdrawResponse
{
    public int Id { get; set; }
    public RukassaPaymentStatus Status { get; set; } = null!;
}
