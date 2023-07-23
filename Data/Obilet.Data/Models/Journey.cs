using Newtonsoft.Json;

namespace Obilet.Data.Models;

public class Journey
{
    public string Kind { get; set; }
    public string Code { get; set; }
    public string Origin { get; set; }
    public string Destination { get; set; }
    public DateTime Departure { get; set; }
    public DateTime Arrival { get; set; }
    
    [JsonProperty("original-price")]
    public decimal OriginalPrice { get; set; }
    
    [JsonProperty("internet-price")]
    public decimal InternetPrice { get; set; }

    
}