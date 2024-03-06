using Newtonsoft.Json;

namespace Construct.Rukassa;

public record RukassaGetWithdrawInfoRequest
{
    [JsonProperty("id")] public int Id { get; set; }
    [JsonProperty("shop_id")] public int ShopId { get; set; }
}
