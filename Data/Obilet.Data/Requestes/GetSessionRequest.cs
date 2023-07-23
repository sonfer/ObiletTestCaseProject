using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace Obilet.Data.Requestes;

public class GetSessionRequest
{
    [JsonProperty("type")] public int Type { get; set; }

    [JsonProperty("connection")] public Connection Connection { get; set; }
    [JsonProperty("browser")] public Browser Browser { get; set; }
}

public class Connection
{
    [JsonProperty("ip-address")] public string Ipaddress { get; set; }

    [JsonProperty("port")] public string Port { get; set; }
}

public class Browser
{
    [JsonProperty("name")] public string Name { get; set; }
    [JsonProperty("version")] public string Version { get; set; }
}