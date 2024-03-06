namespace Construct.Rukassa;

public record RukassaPaymentMethod
{
    private RukassaPaymentMethod(string value) => this.Value = value;

    public string Value { get; private set; }

    public static RukassaPaymentMethod Card { get => new RukassaPaymentMethod("card"); }
    public static RukassaPaymentMethod CardKzt { get => new RukassaPaymentMethod("card_kzt"); }
    public static RukassaPaymentMethod CardUzs { get => new RukassaPaymentMethod("card_uzs"); }
    public static RukassaPaymentMethod YandexMoney { get => new RukassaPaymentMethod("yandexmoney"); }
    public static RukassaPaymentMethod Payeer { get => new RukassaPaymentMethod("payeer"); }
    public static RukassaPaymentMethod Crypto { get => new RukassaPaymentMethod("crypta"); }
    public static RukassaPaymentMethod Sbp { get => new RukassaPaymentMethod("sbp"); }

    public static RukassaPaymentMethod FromString(string s)
    {
        return s switch
        {
            "card" => Card,
            "card_kzt" => CardKzt,
            "card_uzs" => CardUzs,
            "yandexmoney" => YandexMoney,
            "payeer" => Payeer,
            "crypta" => Crypto,
            "sbp" => Sbp,
            _ => throw new ArgumentException("Rukassa returned wrong value of payment method")
        };
    }
}
