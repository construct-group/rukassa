namespace Construct.Rukassa;

public record RukassaGetWithdrawInfoResponse
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public double Amount { get; set; }
    public double Fee { get; set; }
    public string Way { get; set; } = null!;
    public RukassaPaymentWhoFee WhoFee { get; set; } = null!;
    public RukassaPaymentStatus Status { get; set; } = null!;
}
