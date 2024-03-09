namespace Construct.Rukassa;

public class RukassaPaymentNotificationCallbackRequest
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public double Amount { get; set; }
    public string Way { get; set; } = null!;
    public RukassaPaymentStatus Status { get; set; } = null!;
}
