using Newtonsoft.Json;

namespace Obilet.Data.Requestes;

public class GetJourneyRequest
{
    [JsonProperty("device-session")]
    public DeviceSession DeviceSession { get; set; }

    [JsonProperty("date")]
    public DateTime? Date { get; set; }
    
    [JsonProperty("language")]
    public string Language { get; set; }

    [JsonProperty("data")]
    public JourneyRequestData Data { get; set; }
}

public class JourneyRequestData
{
    [JsonProperty("origin-id")]
    public int OriginId { get; set; }

    [JsonProperty("destination-id")]
    public int DestinationId { get; set; }

    [JsonProperty("departure-date")]
    public DateTime DepartureDate { get; set; }
}