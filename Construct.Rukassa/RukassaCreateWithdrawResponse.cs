namespace Construct.Rukassa;

public record RukassaCreateWithdrawResponse
{
    public int Id { get; set; }
    public RukassaPaymentStatus Status { get; set; } = null!;
}
