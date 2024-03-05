namespace Construct.Rukassa;

public record RukassaPaymentCreationRequest<T> where T : RukassaRequestData
{
    public int OrderId { get; set; } 
    public double Amount { get; set; }
    public T? Data { get; set; }
    public RukassaPaymentMethod? Method { get; set; }
    public RukassaPaymentCurrency? Currency { get; set; }
    public string? UserCode { get; set; }
    public bool? Json { get; set; }
}
