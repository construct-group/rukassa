namespace Construct.Rukassa;

public record RukassaPaymentCurrency
{
    private RukassaPaymentCurrency(string value) => this.Value = value;

    public string Value { get; set; }

    public static RukassaPaymentCurrency USD { get => new RukassaPaymentCurrency("USD"); }
    public static RukassaPaymentCurrency RUB { get => new RukassaPaymentCurrency("RUB"); }
}
