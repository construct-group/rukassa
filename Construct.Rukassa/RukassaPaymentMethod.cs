namespace Construct.Rukassa;

public record RukassaPaymentMethod
{
    private RukassaPaymentMethod(string value) => this.Value = value;

    public string Value { get; set; }

    public static RukassaPaymentMethod Card { get => new RukassaPaymentMethod("card"); }
    public static RukassaPaymentMethod CardKzt { get => new RukassaPaymentMethod("card_kzt"); }
    public static RukassaPaymentMethod CardUzs { get => new RukassaPaymentMethod("card_uzs"); }
    public static RukassaPaymentMethod YandexMoney { get => new RukassaPaymentMethod("yandexmoney"); }
    public static RukassaPaymentMethod Payeer { get => new RukassaPaymentMethod("payeer"); }
    public static RukassaPaymentMethod Crypto { get => new RukassaPaymentMethod("crypta"); }
    public static RukassaPaymentMethod Sbp { get => new RukassaPaymentMethod("sbp"); }
}
