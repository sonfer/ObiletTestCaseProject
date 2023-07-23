using Newtonsoft.Json;

namespace Obilet.Data.Responses;

public class BaseResponse<T>
{
    [JsonProperty("status")]
    public string Status { get; set; }
    [JsonProperty("data")]
    public T Data { get; set; }
    [JsonProperty("message")]
    public string Message { get; set; }
    [JsonProperty("user-message")]
    public string UserMessage { get; set; }
    [JsonProperty("api-request-id")]
    public string ApiRequestId { get; set; }
    [JsonProperty("controller")]
    public string Controller { get; set; }
    [JsonProperty("client-request-id")]
    public string ClientRequestId { get; set; }
    [JsonProperty("web-correlation-id")]
    public string WebCorrelationId { get; set; }
    [JsonProperty("correlation-id")]
    public Guid CorrelationId { get; set; }
    
}