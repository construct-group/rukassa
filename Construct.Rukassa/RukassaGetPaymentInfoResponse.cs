namespace Construct.Rukassa;

public record RukassaGetPaymentInfoResponse
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public double Amount { get; set; }
    public double InAmount { get; set; }
    public RukassaPaymentCurrency Currency { get; set; } = null!;
    public RukassaPaymentMethod Method { get; set; } = null!;
    public RukassaPaymentStatus Status { get; set; } = null!;
    public RukassaRequestData Data { get; set; } = null!;
}
