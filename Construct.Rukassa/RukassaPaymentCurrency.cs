namespace Construct.Rukassa;

public record RukassaPaymentCurrency
{
    private RukassaPaymentCurrency(string value) => this.Value = value;

    public string Value { get; private set; }

    public static RukassaPaymentCurrency USD { get => new RukassaPaymentCurrency("USD"); }
    public static RukassaPaymentCurrency RUB { get => new RukassaPaymentCurrency("RUB"); }

    public static RukassaPaymentCurrency FromString(string s)
    {
        return s switch
        {
            "USD" => USD,
            "RUB" => RUB,
            _ => throw new ArgumentException("Rukassa returned wrong value of currency")
        };
    }
}
