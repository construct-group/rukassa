namespace Construct.Rukassa;

public record RukassaConfigurationParameters
{
    public long ShopId { get; set; }
    public string Token { get; set; } = null!;
    public Uri BaseUrl { get; set; } = new Uri("https://lk.rukassa.is/api/v1");
}
