using Newtonsoft.Json;

namespace Construct.Rukassa;

public record RukassaErrorResponse
{
    [JsonProperty("error")] public int Code { get; set; }
    [JsonProperty("message")] public string Message { get; set; } = null!;
}
