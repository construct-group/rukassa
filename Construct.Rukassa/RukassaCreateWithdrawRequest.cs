namespace Construct.Rukassa;

public record RukassaCreateWithdrawRequest
{
    public RukassaWithdrawSystem Way { get; set; }
    public RukassaWriteoffAccount? From { get; set; }
    public string Wallet { get; set; } = null!;
    public double Amount { get; set; }
    public int? OrderId { get; set; }
    public RukassaPaymentWhoFee? WhoFee { get; set; }
    public int? Bank { get; set; }
}
