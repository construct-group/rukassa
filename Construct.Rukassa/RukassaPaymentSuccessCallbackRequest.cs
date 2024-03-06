namespace Construct.Rukassa;

public record RukassaPaymentSuccessCallbackRequest
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public double Amount { get; set; }
    public double InAmount { get; set; }
    public RukassaRequestData Data { get; set; } = null!;
    public DateTime CreatedDateTime { get; set; }
    public RukassaPaymentStatus Status { get; set; } = null!;
}
