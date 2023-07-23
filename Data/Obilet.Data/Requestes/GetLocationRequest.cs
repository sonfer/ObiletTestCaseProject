using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Obilet.Data.Requestes;

public class GetLocationRequest
{
    [JsonProperty("data")]
    public string Data { get; set; }
    
    [JsonProperty("device-session")]
    public DeviceSession DeviceSession { get; set; }
    
    [JsonProperty("date")]
    public DateTime Date { get; set; }
    
    [JsonProperty("language")]
    public string Language { get; set; }
}

public class DeviceSession
{
    [JsonProperty("session-id")]
    public string SessionId { get; set; }
    [JsonProperty("device-id")]
    public string DeviceId { get; set; }
}