using Newtonsoft.Json;

namespace Construct.Rukassa;

public record RukassaGetBalanceResponse
{
    [JsonProperty("balance_rub")] public double BalanceRub { get; set; }
    [JsonProperty("balance_usd")] public double BalanceUsd { get; set; }
}
