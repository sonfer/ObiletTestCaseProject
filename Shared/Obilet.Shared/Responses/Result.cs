namespace Obilet.Shared.Responses;

public class Result: IResult
{
    public bool Success { get; set; }
    public string Status { get; set; }
    public string Message { get; set; }

    protected Result(bool success)
    {
        Success = success;
    }

    public Result(bool success, string status, string message) : this(success)
    {
        Status = status;
        Message = message;
    }
}