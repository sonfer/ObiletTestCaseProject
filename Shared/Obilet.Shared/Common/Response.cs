using System.Runtime.Serialization;
using Microsoft.AspNetCore.Http;

namespace Obilet.Shared.Common;

[DataContract(Name = "{0}")]
public class Response<T>
{
    public Response()
    {
        Messages = new Dictionary<string, string>();
    }

    [DataMember] public ServiceResponseStatuses Status { get; set; }

    [DataMember] public T Data { get; set; }

    [DataMember] public Dictionary<string, string> Messages { get; set; }

    [DataMember] public int StatusCode { get; set; }

    [DataMember] public int TotalCount { get; set; } = 0;
    
    

    #region | Helper Methods |

    public static Response<T> CreateResponse(T data, Dictionary<string, string> messages, int totalCount,
        int statusCode)
    {
        var response = new Response<T>
        {
            Status = ServiceResponseStatuses.Success, Data = data, StatusCode = statusCode, TotalCount = totalCount,
            Messages = messages
        };
        return response;
    }

    public static Response<T> CreateResponse(T data, int totalCount, int statusCode)
    {
        var response = new Response<T>
            { Status = ServiceResponseStatuses.Success, Data = data, StatusCode = statusCode, TotalCount = totalCount };
        return response;
    }

    public static Response<T> CreateResponse(T data, int statusCode)
    {
        var response = new Response<T>
            { Status = ServiceResponseStatuses.Success, Data = data, StatusCode = statusCode };
        return response;
    }

    public static Response<T> CreateResponse(int statusCode)
    {
        var response = new Response<T>
            { Status = ServiceResponseStatuses.Success, Data = default(T), StatusCode = statusCode };
        return response;
    }

    public static Response<T> WarningResponse(T data, string messageKey, string message, int statusCode)
    {
        var dictionary = new Dictionary<string, string> { { messageKey, message } };

        var response = new Response<T>
            { Status = ServiceResponseStatuses.Success, Data = data, Messages = dictionary, StatusCode = statusCode };
        return response;
    }

    public static Response<T> WarningResponse(T data, Dictionary<string, string> messages, int statusCode)
    {
        var response = new Response<T>
            { Status = ServiceResponseStatuses.Success, Data = data, Messages = messages, StatusCode = statusCode };
        return response;
    }


    public static Response<T> ErrorResponse(string messageKey, string message, int statusCode)
    {
        var dictionary = new Dictionary<string, string> { { messageKey, message } };

        var response = new Response<T>
            { Status = ServiceResponseStatuses.Error, Messages = dictionary, StatusCode = statusCode };
        return response;
    }

    public static Response<T> ErrorResponse(Dictionary<string, string> messages, int statusCode)
    {
        var response = new Response<T>
            { Status = ServiceResponseStatuses.Error, Messages = messages, StatusCode = statusCode };
        return response;
    }

    #endregion
    
    public void Success(int statusCode)
    {
        this.StatusCode = statusCode;
        this.Status = ServiceResponseStatuses.Success;
    }
    public void Success(T data, int statusCode)
    {
        this.Data = data;
        this.Success(statusCode);
    }
    
    public bool IsSuccessful()
    {
        this.StatusCode = StatusCodes.Status200OK;
        return this.Status == ServiceResponseStatuses.Success;
    }
    
    public void AddMessage(string messageKey, string message)
    {
        if (this.Messages == null)
        {
            this.Messages = new Dictionary<string, string>();
        }

        var alreadyExists = this.Messages.Any(eachMessage => string.Compare(eachMessage.Key, messageKey, StringComparison.OrdinalIgnoreCase) == 0);

        if (!alreadyExists)
        {
            this.Messages.Add(messageKey, message);
        }
    }

}