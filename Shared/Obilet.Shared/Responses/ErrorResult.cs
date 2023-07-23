namespace Obilet.Shared.Responses;

public class ErrorResult : Result
{
    public ErrorResult() : base(false)
    {

    }

    public ErrorResult(string message, string status) : base(false, message, status)
    {

    }
}