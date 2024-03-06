﻿namespace Construct.Rukassa;

public record RukassaPaymentWhoFee
{
    private RukassaPaymentWhoFee(string value) => this.Value = value;

    public string Value { get; private set; }

    public static RukassaPaymentWhoFee Invoice { get => new("INVOICE"); }
    public static RukassaPaymentWhoFee Balance { get => new("BALANCE"); }

    public static RukassaPaymentWhoFee FromString(string s)
    {
        return s switch
        {
            "INVOICE" => Invoice,
            "BALANCE" => Balance,
            _ => throw new ArgumentException("Rukassa returned wrong value of who pays fee")
        };
    }
}
