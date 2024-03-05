using Newtonsoft.Json;

namespace Construct.Rukassa;

public class RukassaPaymentCreationResponse
{
    [JsonProperty("id")] public int Id { get; set; }
    [JsonProperty("hash")] public string Hash { get; set; } = null!;
    [JsonProperty("url")] public string Url { get; set; } = null!;
}
