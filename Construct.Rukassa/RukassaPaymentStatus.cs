namespace Construct.Rukassa;

public record RukassaPaymentStatus
{
    private RukassaPaymentStatus(string value) => this.Value = value;

    public string Value { get; private set; }

    public static RukassaPaymentStatus Paid { get => new RukassaPaymentStatus("PAID"); }
    public static RukassaPaymentStatus Wait { get => new RukassaPaymentStatus("WAIT"); }
    public static RukassaPaymentStatus Cancel { get => new RukassaPaymentStatus("CANCEL"); }
    public static RukassaPaymentStatus InProgress { get => new RukassaPaymentStatus("IN PROCESS"); }

    public static RukassaPaymentStatus FromString(string s)
    {
        return s switch
        {
            "PAID" => Paid,
            "WAIT" => Wait,
            "CANCEL" => Cancel,
            "IN PROCESS" => InProgress,
            _ => throw new ArgumentException("Rukassa returned wrong value of status")
        };
    }
}
