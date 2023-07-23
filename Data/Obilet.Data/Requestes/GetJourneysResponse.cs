using Newtonsoft.Json;
using Obilet.Data.Models;

namespace Obilet.Data.Requestes;

public class GetJourneysResponse
{
    public int Id { get; set; }
    
    [JsonProperty("partner-id")]
    public int PartnerId { get; set; }
    
    [JsonProperty("partner-name")]
    public string PartnerName { get; set; }

    [JsonProperty("journey")]
    public Journey Journey { get; set; }
    
    [JsonProperty("origin-location")]
    public string OriginLocation { get; set; }
    
    [JsonProperty("destination-location")]
    public string DestinationLocation { get; set; }
}

