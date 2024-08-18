using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace Domain.ExternalObjects.DNS.Hetzner;

public class HetznerDnsRecords
{
    [JsonProperty("records")]
    [JsonPropertyName("records")]
    public List<HetznerDnsRecord> Records { get; set; }
}

public class HetznerDnsRecord
{
    [JsonProperty("id")]
    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonProperty("type")]
    [JsonPropertyName("type")]
    public string Type { get; set; }

    [JsonProperty("name")]
    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonProperty("value")]
    [JsonPropertyName("value")]
    public string Value { get; set; }

    [JsonProperty("zone_id")]
    [JsonPropertyName("zone_id")]
    public string ZoneId { get; set; }

    [JsonProperty("created")]
    [JsonPropertyName("created")]
    public string Created { get; set; }

    [JsonProperty("modified")]
    [JsonPropertyName("modified")]
    public string Modified { get; set; }

    [JsonProperty("ttl")]
    [JsonPropertyName("ttl")]
    public int? Ttl { get; set; }
}