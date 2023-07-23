using Newtonsoft.Json;
using Obilet.Shared.Interfaces;

namespace Obilet.Shared.Responses;

public class DataResult<T> : Result, IDataResult<T>
{ 
    public DataResult(bool success, T data) : base(success)
    {
        Data = data;
    }

    [JsonConstructor]
    public DataResult(bool success, string message, string status, T data) : base(success, message, status)
    {
        Data = data;
    }
    
    public T Data { get; set; }
    
}