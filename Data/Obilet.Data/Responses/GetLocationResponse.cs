using Newtonsoft.Json;

namespace Obilet.Data.Responses;

public class GetLocationResponse
{
    public int Id { get; set; }
    
    [JsonProperty("parent-id")]
    public int ParentId { get; set; }
    
    [JsonProperty("town")]
    public string Town { get; set; }
    
    [JsonProperty("name")] public string Name { get; set; }
    [JsonProperty("city-id")] public string CityId { get; set; }
    [JsonProperty("city-name")] public string CityName{ get; set; }
    [JsonProperty("long-name")] public string LongName { get; set; }
    

}